using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DF3DControl.Base;
using DF3DDraw;
using DF3DData.Class;
using Gvitech.CityMaker.FdeCore;
using DF3DEdit.Class;
using DF3DEdit.Service;
using DF3DEdit.Interface;
using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.RenderControl;
using Gvitech.CityMaker.Resource;
using Gvitech.CityMaker.Common;
using Gvitech.CityMaker.Math;
using System.IO;
using DFCommon.Class;

namespace DF3DEdit.Frm
{
    public class FrmAdd3DModel : XtraForm
    {
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private ButtonEdit beFilePath;
        private ComboBoxEdit comboBoxEdit1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private SpinEdit seZ;
        private SpinEdit seY;
        private SpinEdit seX;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
    

        private void InitializeComponent()
        {
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.seZ = new DevExpress.XtraEditors.SpinEdit();
            this.seY = new DevExpress.XtraEditors.SpinEdit();
            this.seX = new DevExpress.XtraEditors.SpinEdit();
            this.beFilePath = new DevExpress.XtraEditors.ButtonEdit();
            this.comboBoxEdit1 = new DevExpress.XtraEditors.ComboBoxEdit();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.seZ.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.seY.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.seX.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.beFilePath.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.seZ);
            this.layoutControl1.Controls.Add(this.seY);
            this.layoutControl1.Controls.Add(this.seX);
            this.layoutControl1.Controls.Add(this.beFilePath);
            this.layoutControl1.Controls.Add(this.comboBoxEdit1);
            this.layoutControl1.Controls.Add(this.gridControl1);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(207, 453);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // seZ
            // 
            this.seZ.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.seZ.Location = new System.Drawing.Point(68, 400);
            this.seZ.Name = "seZ";
            this.seZ.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.seZ.Size = new System.Drawing.Size(134, 22);
            this.seZ.StyleController = this.layoutControl1;
            this.seZ.TabIndex = 5;
            // 
            // seY
            // 
            this.seY.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.seY.Location = new System.Drawing.Point(68, 374);
            this.seY.Name = "seY";
            this.seY.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.seY.Size = new System.Drawing.Size(134, 22);
            this.seY.StyleController = this.layoutControl1;
            this.seY.TabIndex = 4;
            // 
            // seX
            // 
            this.seX.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.seX.Location = new System.Drawing.Point(68, 348);
            this.seX.Name = "seX";
            this.seX.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.seX.Size = new System.Drawing.Size(134, 22);
            this.seX.StyleController = this.layoutControl1;
            this.seX.TabIndex = 3;
            // 
            // beFilePath
            // 
            this.beFilePath.Location = new System.Drawing.Point(68, 426);
            this.beFilePath.Name = "beFilePath";
            this.beFilePath.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.beFilePath.Size = new System.Drawing.Size(134, 22);
            this.beFilePath.StyleController = this.layoutControl1;
            this.beFilePath.TabIndex = 2;
            this.beFilePath.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.beFilePath_ButtonClick);
            // 
            // comboBoxEdit1
            // 
            this.comboBoxEdit1.EditValue = "鼠标指定";
            this.comboBoxEdit1.Location = new System.Drawing.Point(68, 322);
            this.comboBoxEdit1.Name = "comboBoxEdit1";
            this.comboBoxEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxEdit1.Properties.Items.AddRange(new object[] {
            "鼠标指定",
            "坐标指定"});
            this.comboBoxEdit1.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.comboBoxEdit1.Size = new System.Drawing.Size(134, 22);
            this.comboBoxEdit1.StyleController = this.layoutControl1;
            this.comboBoxEdit1.TabIndex = 1;
            this.comboBoxEdit1.SelectedIndexChanged += new System.EventHandler(this.comboBoxEdit1_SelectedIndexChanged);
            // 
            // gridControl1
            // 
            this.gridControl1.Location = new System.Drawing.Point(5, 25);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(197, 267);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ShowColumnHeaders = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.OptionsView.ShowIndicator = false;
            this.gridView1.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gridView1_CellValueChanged);
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "属性";
            this.gridColumn1.FieldName = "FN";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "值";
            this.gridColumn2.FieldName = "FV";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "字段";
            this.gridColumn3.FieldName = "F";
            this.gridColumn3.Name = "gridColumn3";
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup3,
            this.layoutControlGroup2});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(207, 453);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlGroup3
            // 
            this.layoutControlGroup3.CustomizationFormText = "设置";
            this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2,
            this.layoutControlItem5,
            this.layoutControlItem3,
            this.layoutControlItem6,
            this.layoutControlItem4});
            this.layoutControlGroup3.Location = new System.Drawing.Point(0, 297);
            this.layoutControlGroup3.Name = "layoutControlGroup3";
            this.layoutControlGroup3.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup3.Size = new System.Drawing.Size(207, 156);
            this.layoutControlGroup3.Text = "设置";
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.comboBoxEdit1;
            this.layoutControlItem2.CustomizationFormText = "创建方式：";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(201, 26);
            this.layoutControlItem2.Text = "创建方式：";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(60, 14);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.seX;
            this.layoutControlItem5.CustomizationFormText = "X:";
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(201, 26);
            this.layoutControlItem5.Text = "X:";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(60, 14);
            this.layoutControlItem5.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.seY;
            this.layoutControlItem3.CustomizationFormText = "Y:";
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 52);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(201, 26);
            this.layoutControlItem3.Text = "Y:";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(60, 14);
            this.layoutControlItem3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.seZ;
            this.layoutControlItem6.CustomizationFormText = "Z:";
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 78);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(201, 26);
            this.layoutControlItem6.Text = "Z:";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(60, 14);
            this.layoutControlItem6.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.beFilePath;
            this.layoutControlItem4.CustomizationFormText = "文件路径：";
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 104);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(201, 26);
            this.layoutControlItem4.Text = "文件路径：";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(60, 14);
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.CustomizationFormText = "基本属性";
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup2.Size = new System.Drawing.Size(207, 297);
            this.layoutControlGroup2.Text = "基本属性";
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gridControl1;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(201, 271);
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // FrmAdd3DModel
            // 
            this.ClientSize = new System.Drawing.Size(207, 453);
            this.Controls.Add(this.layoutControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Location = new System.Drawing.Point(10, 150);
            this.Name = "FrmAdd3DModel";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "模型";
            this.TopMost = true;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmAdd3DModel_FormClosed);
            this.Load += new System.EventHandler(this.FrmAdd3DModel_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.seZ.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.seY.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.seX.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.beFilePath.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            this.ResumeLayout(false);

        }
        
        public FrmAdd3DModel()
        {
            InitializeComponent();
        }

        public void SetBirthDay(DateTime dt)
        {
            this.time = dt;
        }

        private DF3DApplication app;
        private DrawTool _drawTool;
        private DateTime time;
        private DataTable _dt;

        private void FrmAdd3DModel_Load(object sender, EventArgs e)
        {
            app = DF3DApplication.Application;
            if (app == null || app.Current3DMapControl == null || app.Workbench == null)
            {
                this.Enabled = false;
                return;
            }

            this._dt = new DataTable();
            this._dt.Columns.Add(new DataColumn("FN", Type.GetType("System.String")));
            this._dt.Columns.Add(new DataColumn("FV", Type.GetType("System.Object")));
            this._dt.Columns.Add(new DataColumn("F", Type.GetType("System.Object")));
            this.gridControl1.DataSource = this._dt;

            LoadProperty();
        }

        private void FrmAdd3DModel_FormClosed(object sender, FormClosedEventArgs e)
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

        private void LoadProperty()
        {
            DF3DFeatureClass featureClassInfo = CommonUtils.Instance().CurEditLayer;
            if (featureClassInfo == null) return;
            IFeatureClass fc = featureClassInfo.GetFeatureClass();
            if (fc == null) return;
            IFieldInfoCollection fiCol = fc.GetFields();
            if (fiCol == null) return;
            for (int i = 0; i < fiCol.Count; i++)
            {
                IFieldInfo fi = fiCol.Get(i);
                if (fi.Name == fc.FidFieldName) continue;

                switch (fi.FieldType)
                {
                    case gviFieldType.gviFieldBlob:
                    case gviFieldType.gviFieldGeometry:
                    case gviFieldType.gviFieldUnknown:
                        continue;
                    //case gviFieldType.gviFieldFloat:
                    //case gviFieldType.gviFieldDouble:                       
                    //    break;
                    //case gviFieldType.gviFieldFID:
                    //case gviFieldType.gviFieldUUID:
                    //case gviFieldType.gviFieldInt16:
                    //case gviFieldType.gviFieldInt32:
                    //case gviFieldType.gviFieldInt64:
                    //    break;
                    //case gviFieldType.gviFieldString:
                    //    break;
                    //case gviFieldType.gviFieldDate:
                    default:
                        DataRow dr = this._dt.NewRow();
                        dr["FN"] = string.IsNullOrEmpty(fi.Alias) ? fi.Name : fi.Alias;
                        dr["FV"] = null;
                        dr["F"] = fi;
                        this._dt.Rows.Add(dr);
                        break;
                }
            }
        }

        public void Clear()
        {
            if (this._drawTool != null)
            {
                this._drawTool.Close();
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
            if (this._drawTool != null && this._drawTool.GetGeo() != null)
            {
                IGeometry geo = this._drawTool.GetGeo();
                if (geo.GeometryType == gviGeometryType.gviGeometryModelPoint)
                {
                    AddRecord(geo);
                }
            }
        }

        private Dictionary<DF3DFeatureClass, IRowBufferCollection> beforeRowBufferMap = new Dictionary<DF3DFeatureClass, IRowBufferCollection>();
        public System.Collections.Generic.Dictionary<string, IEnvelope> GetModelInfos(IResourceManager resManager, string modelName)
        {
            System.Collections.Generic.Dictionary<string, IEnvelope> dictionary = new System.Collections.Generic.Dictionary<string, IEnvelope>();
            if (resManager == null)
            {
                return dictionary;
            }
            Gvitech.CityMaker.Resource.IModel model = resManager.GetModel(modelName);
            if (model == null)
            {
                return dictionary;
            }
            IEnvelope value = this.Clone(model.Envelope);
            dictionary[modelName] = value;
            System.Runtime.InteropServices.Marshal.ReleaseComObject(model);
            return dictionary;
        }
        public IEnvelope Clone(IEnvelope envelop)
        {
            if (envelop == null)
            {
                return null;
            }
            return new EnvelopeClass
            {
                MinX = envelop.MinX,
                MaxX = envelop.MaxX,
                MinY = envelop.MinY,
                MaxY = envelop.MaxY,
                MinZ = envelop.MinZ,
                MaxZ = envelop.MaxZ
            };
        }
        public System.Collections.Generic.List<string> GetImageNames(IResourceManager resManager)
        {
            System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>();
            if (resManager == null)
            {
                return list;
            }
            EnumResName imageNames = resManager.GetImageNames();
            if (imageNames == null)
            {
                return list;
            }
            while (imageNames.MoveNext())
            {
                string current = imageNames.Current;
                list.Add(current);
            }
            return list;
        }
        private SameNameDlg sameNameDlg = new SameNameDlg();
        private IEnvelope ImportOsg(IFeatureClass fc, string filePath)
        {
            try
            {
                IEnvelope env = null;

                if (fc == null || !File.Exists(filePath)) return env;

                string modelName = Path.GetFileNameWithoutExtension(filePath);
                IResourceManager resourceManager = fc.FeatureDataSet as IResourceManager;
                if (resourceManager == null) return env;
                if (!resourceManager.CheckResourceName(modelName)) return env;
                IResourceFactory resourceFactory = new ResourceFactory();

                System.Collections.Generic.Dictionary<string, IEnvelope> modelInfos = this.GetModelInfos(resourceManager, modelName);
                fc.FeatureDataSet.DataSource.StartEditing();
                if (modelInfos.Keys.Contains(modelName))
                {
                    if (!sameNameDlg.IsApplicatonAll)//非应用于全部
                    {
                        string tipMessage = string.Format("{0}模型已经存在!", modelName);
                        sameNameDlg.TipMessage = tipMessage;
                        sameNameDlg.ShowDialog();
                    }
                    if (sameNameDlg.IsOverWriter)//如果覆盖就先删除
                    {
                        resourceManager.DeleteModel(modelName);
                        modelInfos.Remove(modelName);
                    }
                    else
                    {
                        env = modelInfos[modelName];
                    }
                }
                if (!modelInfos.Keys.Contains(modelName))
                {
                    Gvitech.CityMaker.Resource.IModel simplifiedModel = null;
                    Gvitech.CityMaker.Resource.IModel model = null;
                    IMatrix matrix = null;
                    IPropertySet propertySet;
                    resourceFactory.CreateModelAndImageFromFileEx(filePath, out propertySet, out simplifiedModel, out model, out matrix);
                    if (model == null || model.GroupCount == 0) return env;
                    if (!resourceManager.AddModel(modelName, model, simplifiedModel)) return env;//向资源中添加
                    modelInfos[modelName] = model.Envelope;
                    env = modelInfos[modelName];

                    if (propertySet != null && propertySet.Count > 0)
                    {
                        //获得资源的图片
                        System.Collections.Generic.List<string> imageNames = GetImageNames(resourceManager);
                        string[] allKeys = propertySet.GetAllKeys();
                        string[] array = allKeys;
                        for (int i = 0; i < array.Length; i++)
                        {
                            string text2 = array[i];
                            if (imageNames.Contains(text2))
                            {
                                if (!sameNameDlg.IsApplicatonAll)//非应用于全部
                                {
                                    string tipMessage2 = string.Format("{0}贴图已存在!", text2);
                                    sameNameDlg.TipMessage = tipMessage2;
                                    sameNameDlg.ShowDialog();
                                }
                                if (sameNameDlg.IsOverWriter)//是否覆盖
                                {
                                    resourceManager.DeleteImage(text2);
                                    imageNames.Remove(text2);
                                }
                            }

                            if (!imageNames.Contains(text2))
                            {
                                if (!resourceManager.CheckResourceName(text2))
                                {
                                }
                                else
                                {
                                    imageNames.Add(text2);
                                    Gvitech.CityMaker.Resource.IImage image = propertySet.GetProperty(text2) as Gvitech.CityMaker.Resource.IImage;
                                    resourceManager.AddImage(text2, image);
                                }
                            }
                        }
                    }
                }
                fc.FeatureDataSet.DataSource.StopEditing(true);
                return env;
            }
            catch (Exception ex)
            {
                fc.FeatureDataSet.DataSource.StopEditing(false);
                return null;
            }
        }

        private void AddRecord(IGeometry geo)
        {
            try
            {
                if (geo.GeometryType != gviGeometryType.gviGeometryModelPoint) return;

                this.beforeRowBufferMap.Clear();
                SelectCollection.Instance().Clear();
                DF3DFeatureClass featureClassInfo = CommonUtils.Instance().CurEditLayer;
                if (featureClassInfo == null) return;
                IFeatureClass fc = featureClassInfo.GetFeatureClass();
                if (fc == null) return;
                IFeatureDataSet fds = fc.FeatureDataSet;
                if (fds == null) return;
                IFeatureLayer fl = featureClassInfo.GetFeatureLayer();
                if (fl == null) return;
                IFieldInfoCollection fields = fc.GetFields();
                int indexGeo = fields.IndexOf(fl.GeometryFieldName);
                if (indexGeo == -1) return;
                IFieldInfo fiGeo = fields.Get(indexGeo);
                if (fiGeo == null || fiGeo.GeometryDef == null) return;

                IModelPoint pt = geo as IModelPoint;
                string mname = Path.GetFileNameWithoutExtension(pt.ModelName);
                IEnvelope envelope = ImportOsg(fc, pt.ModelName);
                if (envelope == null) return;
                IModelPoint geoOut = pt.Clone2(fiGeo.GeometryDef.VertexAttribute) as IModelPoint;
                if(fiGeo.GeometryDef.HasZ) geoOut.SetCoords(pt.X, pt.Y, pt.Z, 0, 0);
                else geoOut.SetCoords(pt.X, pt.Y, 0, 0, 0);
                geoOut.ModelName = mname;
                geoOut.ModelEnvelope = new EnvelopeClass
                {
                    MinX = envelope.MinX,
                    MaxX = envelope.MaxX,
                    MinY = envelope.MinY,
                    MaxY = envelope.MaxY,
                    MinZ = envelope.MinZ,
                    MaxZ = envelope.MaxZ
                };

                IRowBufferCollection rowCol = new RowBufferCollection();
                IRowBufferFactory fac = new RowBufferFactory();
                IRowBuffer row = fac.CreateRowBuffer(fields);
                row.SetValue(indexGeo, geoOut);
                foreach (DataRow dr in this._dt.Rows)
                {
                    IFieldInfo fi = dr["F"] as IFieldInfo;
                    if (fi == null) continue;
                    string fn = fi.Name;
                    int index = fields.IndexOf(fn);
                    if (index != -1)
                    {
                        if (dr["FV"] == null) row.SetNull(index);
                        else
                        {
                            string strobj = dr["FV"].ToString();
                            bool bRes = false;
                            switch (fi.FieldType)
                            {
                                case gviFieldType.gviFieldBlob:
                                case gviFieldType.gviFieldGeometry:
                                case gviFieldType.gviFieldUnknown:
                                    break;
                                case gviFieldType.gviFieldFloat:
                                    float f;
                                    bRes = float.TryParse(strobj, out f);
                                    if (bRes)
                                    {
                                        row.SetValue(index, f);
                                    }
                                    else
                                    {
                                        row.SetNull(index);
                                    }
                                    break;
                                case gviFieldType.gviFieldDouble:
                                    double d;
                                    bRes = double.TryParse(strobj, out d);
                                    if (bRes)
                                    {
                                        row.SetValue(index, d);
                                    }
                                    else
                                    {
                                        row.SetNull(index);
                                    }
                                    break;
                                case gviFieldType.gviFieldFID:
                                case gviFieldType.gviFieldUUID:
                                case gviFieldType.gviFieldInt16:
                                    Int16 i16;
                                    bRes = Int16.TryParse(strobj, out i16);
                                    if (bRes)
                                    {
                                        row.SetValue(index, i16);
                                    }
                                    else
                                    {
                                        row.SetNull(index);
                                    }
                                    break;
                                case gviFieldType.gviFieldInt32:
                                    Int32 i32;
                                    bRes = Int32.TryParse(strobj, out i32);
                                    if (bRes)
                                    {
                                        row.SetValue(index, i32);
                                    }
                                    else
                                    {
                                        row.SetNull(index);
                                    }
                                    break;
                                case gviFieldType.gviFieldInt64:
                                    Int64 i64;
                                    bRes = Int64.TryParse(strobj, out i64);
                                    if (bRes)
                                    {
                                        row.SetValue(index, i64);
                                    }
                                    else
                                    {
                                        row.SetNull(index);
                                    }
                                    break; ;
                                case gviFieldType.gviFieldString:
                                    row.SetValue(index, strobj);
                                    break;
                                case gviFieldType.gviFieldDate:
                                    DateTime dt;
                                    bRes = DateTime.TryParse(strobj, out dt);
                                    if (bRes)
                                    {
                                        row.SetValue(index, dt);
                                    }
                                    else
                                    {
                                        row.SetNull(index);
                                    }
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                }
                rowCol.Add(row);
                beforeRowBufferMap[featureClassInfo] = rowCol;
                UpdateDatabase();
                Clear();
                (this._drawTool as Draw3DModel).Set3DModelFilePath(this.beFilePath.Text);
            }
            catch (Exception ex)
            {
            }
        }
        private void UpdateDatabase()
        {
            DF3DFeatureClass featureClassInfo = CommonUtils.Instance().CurEditLayer;
            if (featureClassInfo == null) return;
            IFeatureClass fc = featureClassInfo.GetFeatureClass();
            if (fc == null) return;
            int count = 1;
            EditParameters editParameters = new EditParameters(fc.Guid.ToString());
            editParameters.connectionInfo = CommonUtils.Instance().GetCurrentFeatureDataset().DataSource.ConnectionInfo.ToConnectionString();
            editParameters.datasetName = CommonUtils.Instance().GetCurrentFeatureDataset().Name;
            editParameters.geometryMap = beforeRowBufferMap;
            editParameters.nTotalCount = count;
            editParameters.TemproalTime = this.time;
            IBatcheEdit batcheEdit = BatchEditFactory.CreateBatchEdit(count);
            batcheEdit.BeginEdit();
            batcheEdit.DoWork(EditType.ET_INSERT_FEATURES, editParameters);
            batcheEdit.EndEdit();
        }

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                if (e.RowHandle == -1) return;
                if (e.Column.FieldName == "FV")
                {
                    DataRow dr = this.gridView1.GetDataRow(e.RowHandle);
                    IFieldInfo fi = dr["F"] as IFieldInfo;
                    if (fi != null)
                    {
                        string strobj = e.Value.ToString();
                        bool bRes = false;
                        switch (fi.FieldType)
                        {
                            case gviFieldType.gviFieldBlob:
                            case gviFieldType.gviFieldGeometry:
                            case gviFieldType.gviFieldUnknown:
                                break;
                            case gviFieldType.gviFieldFloat:
                                float f;
                                bRes = float.TryParse(strobj, out f);
                                if (bRes)
                                {
                                    dr["FV"] = strobj;
                                }
                                else
                                {
                                    dr["FV"] = null;
                                }
                                break;
                            case gviFieldType.gviFieldDouble:
                                double d;
                                bRes = double.TryParse(strobj, out d);
                                if (bRes)
                                {
                                    dr["FV"] = strobj;
                                }
                                else
                                {
                                    dr["FV"] = null;
                                }
                                break;
                            case gviFieldType.gviFieldFID:
                            case gviFieldType.gviFieldUUID:
                            case gviFieldType.gviFieldInt16:
                                Int16 i16;
                                bRes = Int16.TryParse(strobj, out i16);
                                if (bRes)
                                {
                                    dr["FV"] = strobj;
                                }
                                else
                                {
                                    dr["FV"] = null;
                                }
                                break;
                            case gviFieldType.gviFieldInt32:
                                Int32 i32;
                                bRes = Int32.TryParse(strobj, out i32);
                                if (bRes)
                                {
                                    dr["FV"] = strobj;
                                }
                                else
                                {
                                    dr["FV"] = null;
                                }
                                break;
                            case gviFieldType.gviFieldInt64:
                                Int64 i64;
                                bRes = Int64.TryParse(strobj, out i64);
                                if (bRes)
                                {
                                    dr["FV"] = strobj;
                                }
                                else
                                {
                                    dr["FV"] = null;
                                }
                                break; ;
                            case gviFieldType.gviFieldString:
                                dr["FV"] = strobj;
                                break;
                            case gviFieldType.gviFieldDate:
                                DateTime dt;
                                bRes = DateTime.TryParse(strobj, out dt);
                                if (bRes)
                                {
                                    dr["FV"] = strobj;
                                }
                                else
                                {
                                    dr["FV"] = null;
                                }
                                break;
                            default:
                                break;
                        }
                        this.gridView1.RefreshRow(e.RowHandle);
                    }

                }
            }
            catch (Exception ex)
            {
            }
        }

        private void beFilePath_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                if (this.comboBoxEdit1.SelectedIndex == 0)
                {
                    OpenFileDialog ofd = new OpenFileDialog();
                    ofd.DefaultExt = "osg";
                    ofd.Filter = "3DModel Filess(*.osg)|*.osg";
                    ofd.RestoreDirectory = true;
                    if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        this.beFilePath.Text = ofd.FileName;
                        Clear();
                        if (this._drawTool != null)
                        {
                            this._drawTool.OnStartDraw -= new OnStartDraw(this.OnStartDraw);
                            this._drawTool.OnFinishedDraw -= new OnFinishedDraw(this.OnFinishedDraw);
                            this._drawTool.Close();
                            this._drawTool.End();
                            this._drawTool = null;
                        }
                        // 开启
                        this._drawTool = DrawToolService.Instance.CreateDrawTool(DrawType._3DModel);
                        if (this._drawTool != null)
                        {
                            (this._drawTool as Draw3DModel).Set3DModelFilePath(this.beFilePath.Text);
                            this._drawTool.OnStartDraw += new OnStartDraw(this.OnStartDraw);
                            this._drawTool.OnFinishedDraw += new OnFinishedDraw(this.OnFinishedDraw);
                            this._drawTool.Start();
                        }
                    }
                }
                else if (this.comboBoxEdit1.SelectedIndex == 1)
                {
                    OpenFileDialog ofd = new OpenFileDialog();
                    ofd.DefaultExt = "osg";
                    ofd.Filter = "3DModel Filess(*.osg)|*.osg";
                    ofd.RestoreDirectory = true;
                    if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        this.beFilePath.Text = ofd.FileName;
                        double x = double.Parse(this.seX.Text.ToString());
                        double y = double.Parse(this.seY.Text.ToString());
                        double z = double.Parse(this.seZ.Text.ToString());
                        IModelPoint mp = (new GeometryFactory()).CreateGeometry(gviGeometryType.gviGeometryModelPoint, gviVertexAttribute.gviVertexAttributeZM) as IModelPoint;
                        mp.SetCoords(x, y, z, 0, 0);
                        mp.ModelName = this.beFilePath.Text;
                        AddRecord(mp);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void comboBoxEdit1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.comboBoxEdit1.SelectedIndex == 0)
            {
                this.layoutControlItem5.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                this.layoutControlItem3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                this.layoutControlItem6.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }
            else if (this.comboBoxEdit1.SelectedIndex == 1)
            {
                this.layoutControlItem5.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                this.layoutControlItem3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                this.layoutControlItem6.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                Clear();
                if (this._drawTool != null)
                {
                    this._drawTool.OnStartDraw -= new OnStartDraw(this.OnStartDraw);
                    this._drawTool.OnFinishedDraw -= new OnFinishedDraw(this.OnFinishedDraw);
                    this._drawTool.Close();
                    this._drawTool.End();
                    this._drawTool = null;
                }
                IPoint pt = null;
                IEulerAngle ang = null;
                app.Current3DMapControl.Camera.GetCamera2(out pt, out ang);
                if (pt != null)
                {
                    this.seX.EditValue = pt.X;
                    this.seY.EditValue = pt.Y;
                    this.seZ.EditValue = pt.Z;
                }
            }
        }
    }
}
