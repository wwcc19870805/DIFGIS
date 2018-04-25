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
using ESRI.ArcGIS.DataSourcesGDB;
using DFCommon.Class;
using ESRI.ArcGIS.Geometry;
using DF2DControl.Base;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Carto;

namespace DF2DScan.Frm
{
    public partial class FrmSheetLoc : XtraForm
    {
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.SimpleButton btn_Loc;
        private DevExpress.XtraEditors.TextEdit te_sheetName;
        private DevExpress.XtraEditors.ComboBoxEdit cb_sheetList;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        List<string> sheetList;
        private SimpleButton btn_close;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        IFeatureClass fc;
    
        public FrmSheetLoc()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.btn_close = new DevExpress.XtraEditors.SimpleButton();
            this.btn_Loc = new DevExpress.XtraEditors.SimpleButton();
            this.te_sheetName = new DevExpress.XtraEditors.TextEdit();
            this.cb_sheetList = new DevExpress.XtraEditors.ComboBoxEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.te_sheetName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cb_sheetList.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.btn_close);
            this.layoutControl1.Controls.Add(this.btn_Loc);
            this.layoutControl1.Controls.Add(this.te_sheetName);
            this.layoutControl1.Controls.Add(this.cb_sheetList);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(284, 88);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // btn_close
            // 
            this.btn_close.Location = new System.Drawing.Point(144, 57);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(135, 22);
            this.btn_close.StyleController = this.layoutControl1;
            this.btn_close.TabIndex = 7;
            this.btn_close.Text = "关闭";
            this.btn_close.Click += new System.EventHandler(this.btn_close_Click);
            // 
            // btn_Loc
            // 
            this.btn_Loc.Location = new System.Drawing.Point(5, 57);
            this.btn_Loc.Name = "btn_Loc";
            this.btn_Loc.Size = new System.Drawing.Size(135, 22);
            this.btn_Loc.StyleController = this.layoutControl1;
            this.btn_Loc.TabIndex = 6;
            this.btn_Loc.Text = "定位";
            this.btn_Loc.Click += new System.EventHandler(this.btn_Loc_Click);
            // 
            // te_sheetName
            // 
            this.te_sheetName.Location = new System.Drawing.Point(68, 31);
            this.te_sheetName.Name = "te_sheetName";
            this.te_sheetName.Size = new System.Drawing.Size(211, 22);
            this.te_sheetName.StyleController = this.layoutControl1;
            this.te_sheetName.TabIndex = 5;
            // 
            // cb_sheetList
            // 
            this.cb_sheetList.Location = new System.Drawing.Point(68, 5);
            this.cb_sheetList.Name = "cb_sheetList";
            this.cb_sheetList.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cb_sheetList.Size = new System.Drawing.Size(211, 22);
            this.cb_sheetList.StyleController = this.layoutControl1;
            this.cb_sheetList.TabIndex = 4;
            this.cb_sheetList.SelectedIndexChanged += new System.EventHandler(this.cb_sheetList_SelectedIndexChanged);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup2});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(284, 88);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.CustomizationFormText = "layoutControlGroup2";
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem4});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup2.Size = new System.Drawing.Size(284, 88);
            this.layoutControlGroup2.Text = "layoutControlGroup2";
            this.layoutControlGroup2.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.cb_sheetList;
            this.layoutControlItem1.CustomizationFormText = "图幅列表：";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.MaxSize = new System.Drawing.Size(278, 26);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(278, 26);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(278, 26);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.Text = "图幅列表：";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(60, 14);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.te_sheetName;
            this.layoutControlItem2.CustomizationFormText = "图幅名称：";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem2.MaxSize = new System.Drawing.Size(278, 26);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(278, 26);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(278, 26);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.Text = "图幅名称：";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(60, 14);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.btn_Loc;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 52);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(139, 30);
            this.layoutControlItem3.Text = "layoutControlItem3";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.btn_close;
            this.layoutControlItem4.CustomizationFormText = "layoutControlItem4";
            this.layoutControlItem4.Location = new System.Drawing.Point(139, 52);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(139, 30);
            this.layoutControlItem4.Text = "layoutControlItem4";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextToControlDistance = 0;
            this.layoutControlItem4.TextVisible = false;
            // 
            // FrmSheetLoc
            // 
            this.ClientSize = new System.Drawing.Size(284, 88);
            this.Controls.Add(this.layoutControl1);
            this.Name = "FrmSheetLoc";
            this.Text = "图幅定位";
            this.TopMost = true;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmSheetLoc_FormClosed);
            this.Load += new System.EventHandler(this.FrmSheetLoc_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.te_sheetName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cb_sheetList.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            this.ResumeLayout(false);

        }
        
        private void FrmSheetLoc_Load(object sender, EventArgs e)
        {
            try
            {
                fc = LoadMetaData();
                if (fc != null) sheetList = LoadSheet(fc);
                if (sheetList.Count > 0)
                {
                    this.cb_sheetList.Properties.Items.AddRange(sheetList.ToArray());
                }
            }
            catch (System.Exception ex)
            {
            	
            }
           

        }

        private IFeatureClass LoadMetaData()
        {
            string path = Config.GetConfigValue("2DMdbPipe");
            IWorkspaceFactory pWsF = new AccessWorkspaceFactory();
            IFeatureWorkspace pFWs = pWsF.OpenFromFile(path, 0) as IFeatureWorkspace;
            if (pFWs == null) return null;
            IFeatureDataset pFDs = pFWs.OpenFeatureDataset("Assi_10");
            if (pFDs == null) return null;
            IEnumDataset pEnumDs = pFDs.Subsets;
            IDataset fDs;
            IFeatureClass fc = null;
            while ((fDs = pEnumDs.Next()) != null)
            {
                if (fDs.Name == "Metadata")
                {
                    fc = fDs as IFeatureClass;
                }

            }
            return fc;
           
        }

        private List<string> LoadSheet(IFeatureClass fc)
        {
            List<string> sheetList = new List<string>();
            int index = fc.FindField("Maptitle");
            if (index == -1) return null;
            IQueryFilter filter = new QueryFilter();
            filter.SubFields = "Maptitle";
            IFeatureCursor cursor = fc.Search(null,true);
            IFeature feature;
            string mapTitle;
            while((feature = cursor.NextFeature()) != null)
            {
                mapTitle = feature.get_Value(index).ToString();
                if (mapTitle != null)
                    sheetList.Add(mapTitle);
            }
            return sheetList;
            
        }

        private void cb_sheetList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.te_sheetName.Text = cb_sheetList.SelectedItem.ToString();
        }

        private void btn_Loc_Click(object sender, EventArgs e)
        {
            IQueryFilter filter = new QueryFilter();
            filter.WhereClause = "Maptitle = '" + te_sheetName.Text + "'";
            IFeatureCursor cursor = fc.Search(filter, true);
            if (cursor == null) XtraMessageBox.Show("请输入正确的图幅名");
            IFeature feature = cursor.NextFeature();

            int index1 = feature.Fields.FindField("SWX");
            int index2 = feature.Fields.FindField("SWY");
            int index3 = feature.Fields.FindField("NWX");
            int index4 = feature.Fields.FindField("NWY");
            if (index1 == -1 || index2 == -1 || index3 == -1 || index4 == -1) XtraMessageBox.Show("加载图幅坐标出错");
            double swx = Convert.ToDouble(feature.get_Value(index1));
            double swy = Convert.ToDouble(feature.get_Value(index2));
            double nwx = Convert.ToDouble(feature.get_Value(index3));
            double nwy = Convert.ToDouble(feature.get_Value(index4));

            double width = Math.Abs(swy - nwy);
            double length = Math.Abs(swx - nwx);

          
            IPointCollection pCol = new PolygonClass();
            IPoint p1 = new PointClass();
            p1.PutCoords(swx, swy);
            pCol.AddPoint(p1);
            IPoint p2 = new PointClass();
            p2.PutCoords(nwx, swy);
            pCol.AddPoint(p2);
            IPoint p3 = new PointClass();
            p3.PutCoords(nwx,nwy);
            pCol.AddPoint(p3);
            IPoint p4 = new PointClass();
            p4.PutCoords(swx,nwy);
            pCol.AddPoint(p4);
            pCol.AddPoint(p1);
            IPoint pCenter = new PointClass();
            pCenter.PutCoords((swx + nwx) / 2, (swy + nwy) / 2);
            IGeometry geo = pCol as IGeometry;

            DF2DApplication app = DF2DApplication.Application;
            if (app == null || app.Current2DMapControl == null) return;                      
            IGraphicsContainer gc = app.Current2DMapControl.ActiveView.GraphicsContainer;
            AddElement(geo, gc);
            app.Current2DMapControl.MapScale = 1500;
            app.Current2DMapControl.CenterAt(pCenter); 
            app.Current2DMapControl.ActiveView.Refresh();
        }

        private void AddElement(IGeometry geo,IGraphicsContainer gc)
        {
            IElement pElem = new RectangleElement();
            pElem.Geometry = geo;

            ISimpleFillSymbol pSFSym = new SimpleFillSymbol();
            Color color = ColorTranslator.FromHtml(SystemInfo.Instance.FillColor);
            IColor pColor = new RgbColorClass();
            pColor.RGB = color.B * 65536 + color.G * 256 + color.R;
            pSFSym.Color = pColor;
            pSFSym.Style = esriSimpleFillStyle.esriSFSBackwardDiagonal;

            ISimpleLineSymbol pSLSym = new SimpleLineSymbol();
            pSLSym.Style = esriSimpleLineStyle.esriSLSSolid;
            pSLSym.Width = 1;
            pSLSym.Color = pColor;

            pSFSym.Outline = pSLSym;

            IFillShapeElement pElemFillShp = pElem as IFillShapeElement;
            pElemFillShp.Symbol = pSFSym;            
            gc.AddElement(pElem, 0);
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
            DF2DApplication app = DF2DApplication.Application;
            app.Current2DMapControl.ActiveView.GraphicsContainer.DeleteAllElements();
            app.Current2DMapControl.ActiveView.Refresh();
        }

        private void FrmSheetLoc_FormClosed(object sender, FormClosedEventArgs e)
        {
            DF2DApplication app = DF2DApplication.Application;
            app.Current2DMapControl.ActiveView.GraphicsContainer.DeleteAllElements();
            app.Current2DMapControl.ActiveView.Refresh();
        }


    }
}
