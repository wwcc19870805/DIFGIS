using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraLayout;
using DevExpress.XtraLayout.Utils;
using Gvitech.CityMaker.FdeCore;
using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.RenderControl;
using Gvitech.CityMaker.Resource;
using DF3DControl.Base;
using DF3DData.Class;
using DF3DEdit.Class;
using DF3DEdit.Delegate;
using DF3DEdit.Service;

namespace DF3DEdit.Frm
{
    public class SplitDlg : XtraForm
    {
        private IRenderGeometry _renderGeo = null;
        private gviInteractMode _interactaMode;
        private System.ComponentModel.IContainer components = null;
        private LayoutControl layoutControl1;
        private LayoutControlGroup layoutControlGroup1;
        private EmptySpaceItem emptySpaceItem1;
        private SimpleButton sbtn_Reset;
        private SimpleButton sbtn_Cancel;
        private SimpleButton sbtn_Split;
        private RadioGroup radioGroup1;
        private SimpleButton sbtn_BrowseFile;
        private TextEdit textEdit_FilePath;
        private LayoutControlGroup layoutControlGroup2;
        private LayoutControlItem layoutControlItem1;
        private LayoutControlItem layoutControlItem2;
        private LayoutControlItem layoutControlItem3;
        private LayoutControlItem layoutControlItem6;
        private EmptySpaceItem emptySpaceItem2;
        private EmptySpaceItem emptySpaceItem3;
        private LayoutControlItem layoutControlItem4;
        private LayoutControlItem layoutControlItem5;
        public SplitDlg()
        {
            DF3DApplication app = DF3DApplication.Application;
            if (app == null || app.Current3DMapControl == null) return;
            this.InitializeComponent();
            //MainFrmService.RibbonControl.Enabled = false;
            //MainFrmService.LayerTreePanel.Enabled = false;
            //MainFrmService.HideContainerBottom.Enabled = false;
            //MainFrmService.RightPanelContainer.Enabled = false;
            this._interactaMode = app.Current3DMapControl.InteractMode;
            app.Current3DMapControl.InteractMode = gviInteractMode.gviInteractNormal;
            GeometryEdit.Instance().SpatialQueryEditStopEvent += new SpatialQueryEditStopHandle(this.SpatialQueryEditStopEvent);
        }
        private void sbtn_Reset_Click(object sender, System.EventArgs e)
        {
            if (this.radioGroup1.SelectedIndex != -1)
            {
                this.SetMakePolygonMode();
            }
        }
        private void sbtn_BrowseFile_Click(object sender, System.EventArgs e)
        {
            DF3DApplication app = DF3DApplication.Application;
            if (app == null || app.Current3DMapControl == null) return;
            System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog();
            openFileDialog.Multiselect = false;
            openFileDialog.CheckFileExists = true;
            openFileDialog.Filter = "Shp File(*.shp)|*.shp";
            openFileDialog.DefaultExt = ".shp";
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string fileName = openFileDialog.FileName;
                IMultiPolygon multiPolygonFromFile = this.GetMultiPolygonFromFile(fileName);
                if (multiPolygonFromFile != null)
                {
                    IGeometryFactory geometryFactory = new GeometryFactoryClass();
                    IGeometry geometry = multiPolygonFromFile.Clone2(gviVertexAttribute.gviVertexAttributeNone);
                    ISurfaceSymbol surfaceSymbol = new SurfaceSymbolClass();
                    surfaceSymbol.Color = 1442840448u;
                    surfaceSymbol.BoundarySymbol = new CurveSymbolClass
                    {
                        Color = 1442840448u
                    };
                    this._renderGeo = app.Current3DMapControl.ObjectManager.CreateRenderMultiPolygon(geometry as IMultiPolygon, surfaceSymbol, app.Current3DMapControl.ProjectTree.RootID);
                    int lastError = app.Current3DMapControl.GetLastError();
                    string str = string.Empty;
                    if (lastError != 0)
                    {
                        //str = Logger.GetRenderCtrlError(lastError);
                    }
                    if (this._renderGeo == null)
                    {
                        XtraMessageBox.Show("多边形创建失败!" + str);
                    }
                    else
                    {
                        app.Current3DMapControl.HighlightHelper.VisibleMask = 1;
                        app.Current3DMapControl.HighlightHelper.SetRegion(this._renderGeo.GetFdeGeometry());
                        app.Current3DMapControl.Camera.FlyToObject(this._renderGeo.Guid, gviActionCode.gviActionFlyTo);
                        this.textEdit_FilePath.Text = fileName;
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(multiPolygonFromFile);
                    }
                }
            }
        }
        private void sbtn_Split_Click(object sender, System.EventArgs e)
        {
            DF3DApplication app = DF3DApplication.Application;
            if (app == null || app.Current3DMapControl == null) return;
            if (System.Windows.Forms.DialogResult.No != XtraMessageBox.Show("模型拆分不支持撤销操作,是否继续?", "询问", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Exclamation))
            {
                IFeatureClass featureClass = null;
                IFdeCursor fdeCursor = null;
                app.Current3DMapControl.PauseRendering(false);
                WaitDialogForm waitDialogForm = null;
                try
                {
                    int count = SelectCollection.Instance().GetCount(true);
                    if (count <= 0)
                    {
                        XtraMessageBox.Show("未选中要拆分的模型!");
                    }
                    else
                    {
                        if (this._renderGeo == null || this._renderGeo.GetFdeGeometry() == null)
                        {
                            XtraMessageBox.Show("请先绘制或选择拆分多边形!");
                        }
                        else
                        {
                            IMultiPolygon multiPolygon = null;
                            if (this.radioGroup1.SelectedIndex == 1)
                            {
                                IGeometryFactory geometryFactory = new GeometryFactoryClass();
                                multiPolygon = (geometryFactory.CreateGeometry(gviGeometryType.gviGeometryMultiPolygon, gviVertexAttribute.gviVertexAttributeZ) as IMultiPolygon);
                                multiPolygon.AddPolygon(this._renderGeo.GetFdeGeometry() as IPolygon);
                            }
                            else
                            {
                                if (this.radioGroup1.SelectedIndex == 0)
                                {
                                    multiPolygon = (this._renderGeo.GetFdeGeometry() as IMultiPolygon);
                                }
                            }
                            if (multiPolygon == null)
                            {
                                XtraMessageBox.Show("获取裁剪多边形失败!");
                            }
                            else
                            {
                                waitDialogForm = new WaitDialogForm(string.Empty, "正在拆分模型,请稍后...");
                                HashMap featureClassInfoMap = SelectCollection.Instance().FeatureClassInfoMap;
                                System.Collections.Hashtable hashtable = new System.Collections.Hashtable();
                                foreach (DF3DFeatureClass featureClassInfo in featureClassInfoMap.Keys)
                                {
                                    if (featureClassInfo.GetFeatureLayer().GeometryType == gviGeometryColumnType.gviGeometryColumnModelPoint)
                                    {
                                        System.Collections.Generic.List<int> list = new System.Collections.Generic.List<int>();
                                        System.Collections.Generic.List<int> list2 = new System.Collections.Generic.List<int>();
                                        IRowBufferCollection rowBufferCollection = new RowBufferCollectionClass();
                                        IRowBufferCollection rowBufferCollection2 = new RowBufferCollectionClass();
                                        ResultSetInfo resultSetInfo = featureClassInfoMap[featureClassInfo] as ResultSetInfo;
                                        DataTable resultSetTable = resultSetInfo.ResultSetTable;
                                        if (resultSetTable.Rows.Count >= 1)
                                        {
                                            featureClass = featureClassInfo.GetFeatureClass();
                                            IResourceManager resourceManager = featureClass.FeatureDataSet as IResourceManager;
                                            string fidFieldName = featureClass.FidFieldName;
                                            int num = featureClass.GetFields().IndexOf(fidFieldName);
                                            foreach (DataRow dataRow in resultSetTable.Rows)
                                            {
                                                int num2 = int.Parse(dataRow[fidFieldName].ToString());
                                                IRowBuffer row = featureClass.GetRow(num2);
                                                int position = row.FieldIndex(featureClassInfo.GetFeatureLayer().GeometryFieldName);
                                                IModelPoint modelPoint = row.GetValue(position) as IModelPoint;
                                                if (modelPoint != null)
                                                {
                                                    Gvitech.CityMaker.Resource.IModel model = resourceManager.GetModel(modelPoint.ModelName);
                                                    IGeometryConvertor geometryConvertor = new GeometryConvertorClass();
                                                    Gvitech.CityMaker.Resource.IModel model2 = null;
                                                    Gvitech.CityMaker.Resource.IModel model3 = null;
                                                    IModelPoint modelPoint2 = null;
                                                    IModelPoint modelPoint3 = null;
                                                    bool flag = geometryConvertor.SplitModelPointByPolygon2D(multiPolygon, model, modelPoint, out model2, out modelPoint2, out model3, out modelPoint3);
                                                    if (flag && model2 != null && !model2.IsEmpty && modelPoint2 != null && model3 != null && !model3.IsEmpty && modelPoint3 != null)
                                                    {
                                                        System.Guid guid = System.Guid.NewGuid();
                                                        string text = guid.ToString();
                                                        resourceManager.AddModel(text, model2, null);
                                                        resourceManager.RebuildSimplifiedModel(text);
                                                        guid = System.Guid.NewGuid();
                                                        string text2 = guid.ToString();
                                                        resourceManager.AddModel(text2, model3, null);
                                                        resourceManager.RebuildSimplifiedModel(text2);
                                                        modelPoint3.ModelName = text2;
                                                        row.SetValue(position, modelPoint3);
                                                        rowBufferCollection.Add(row);
                                                        list.Add(num2);
                                                        modelPoint2.ModelName = text;
                                                        IRowBuffer rowBuffer = row.Clone(false);
                                                        rowBuffer.SetNull(num);
                                                        rowBuffer.SetValue(position, modelPoint2);
                                                        fdeCursor = featureClass.Insert();
                                                        fdeCursor.InsertRow(rowBuffer);
                                                        int lastInsertId = fdeCursor.LastInsertId;
                                                        rowBuffer.SetValue(num, lastInsertId);
                                                        rowBufferCollection2.Add(rowBuffer);
                                                        list2.Add(lastInsertId);
                                                        System.Runtime.InteropServices.Marshal.ReleaseComObject(fdeCursor);
                                                        fdeCursor = null;
                                                    }
                                                }
                                            }
                                            featureClass.UpdateRows(rowBufferCollection, false);
                                            app.Current3DMapControl.FeatureManager.EditFeatures(featureClass, rowBufferCollection);
                                            app.Current3DMapControl.FeatureManager.CreateFeatures(featureClass, rowBufferCollection2);
                                            hashtable[featureClassInfo] = list2;
                                            //System.Runtime.InteropServices.Marshal.ReleaseComObject(featureClass);
                                            featureClass = null;
                                            System.Runtime.InteropServices.Marshal.ReleaseComObject(resourceManager);
                                            resourceManager = null;
                                        }
                                    }
                                }
                                SelectCollection.Instance().UpdateSelection(hashtable);
                                base.Close();
                                base.DialogResult = System.Windows.Forms.DialogResult.OK;
                            }
                        }
                    }
                }
                catch (System.Runtime.InteropServices.COMException ex)
                {
                    XtraMessageBox.Show(ex.Message);
                }
                catch (System.UnauthorizedAccessException var_35_4EC)
                {
                    XtraMessageBox.Show("拒绝访问");
                }
                catch (System.Exception ex2)
                {
                    XtraMessageBox.Show(ex2.Message);
                }
                finally
                {
                    if (waitDialogForm != null)
                    {
                        waitDialogForm.Close();
                    }
                    if (fdeCursor != null)
                    {
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(fdeCursor);
                        fdeCursor = null;
                    }
                    //if (featureClass != null && System.Runtime.InteropServices.Marshal.IsComObject(featureClass))
                    //{
                    //    System.Runtime.InteropServices.Marshal.ReleaseComObject(featureClass);
                    //    featureClass = null;
                    //}
                    app.Current3DMapControl.ResumeRendering();
                }
            }
        }
        private void sbtn_Cancel_Click(object sender, System.EventArgs e)
        {
            base.Close();
        }
        private void radioGroup1_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            this.SetMakePolygonMode();
        }
        private IMultiPolygon GetMultiPolygonFromFile(string sFilePath)
        {
            IDataSource dataSource = null;
            IFeatureDataSet featureDataSet = null;
            IFeatureClass featureClass = null;
            IFdeCursor fdeCursor = null;
            IMultiPolygon result;
            try
            {
                IConnectionInfo connectionInfo = new ConnectionInfoClass();
                connectionInfo.ConnectionType = gviConnectionType.gviConnectionShapeFile;
                connectionInfo.Database = sFilePath;
                dataSource = ((IDataSourceFactory)new DataSourceFactoryClass()).OpenDataSource(connectionInfo);
                string[] featureDatasetNames = dataSource.GetFeatureDatasetNames();
                if (featureDatasetNames != null && featureDatasetNames.Length > 0)
                {
                    featureDataSet = dataSource.OpenFeatureDataset(featureDatasetNames[0]);
                    string[] namesByType = featureDataSet.GetNamesByType(gviDataSetType.gviDataSetFeatureClassTable);
                    if (namesByType != null && namesByType.Length > 0)
                    {
                        featureClass = featureDataSet.OpenFeatureClass(namesByType[0]);
                        IFieldInfoCollection fields = featureClass.GetFields();
                        int num = -1;
                        gviGeometryColumnType gviGeometryColumnType = gviGeometryColumnType.gviGeometryColumnUnknown;
                        gviVertexAttribute vertexAttribute = gviVertexAttribute.gviVertexAttributeNone;
                        for (int i = 0; i < fields.Count; i++)
                        {
                            IFieldInfo fieldInfo = fields.Get(i);
                            if (fieldInfo.FieldType == gviFieldType.gviFieldGeometry && fieldInfo.GeometryDef != null)
                            {
                                num = i;
                                gviGeometryColumnType = fieldInfo.GeometryDef.GeometryColumnType;
                                vertexAttribute = fieldInfo.GeometryDef.VertexAttribute;
                                break;
                            }
                        }
                        if (num == -1 || gviGeometryColumnType != gviGeometryColumnType.gviGeometryColumnPolygon)
                        {
                            XtraMessageBox.Show("应选择面状矢量数据!");
                            result = null;
                        }
                        else
                        {
                            IGeometryFactory geometryFactory = new GeometryFactoryClass();
                            IMultiPolygon multiPolygon = geometryFactory.CreateGeometry(gviGeometryType.gviGeometryMultiPolygon, vertexAttribute) as IMultiPolygon;
                            multiPolygon.SpatialCRS = featureDataSet.SpatialReference;
                            fdeCursor = featureClass.Search(null, true);
                            IRowBuffer rowBuffer;
                            while ((rowBuffer = fdeCursor.NextRow()) != null)
                            {
                                object value = rowBuffer.GetValue(num);
                                if (value != null && value is IGeometry)
                                {
                                    IGeometry geometry = value as IGeometry;
                                    if (geometry.GeometryType == gviGeometryType.gviGeometryPolygon)
                                    {
                                        multiPolygon.AddPolygon(geometry as IPolygon);
                                    }
                                    else
                                    {
                                        if (geometry.GeometryType == gviGeometryType.gviGeometryMultiPolygon)
                                        {
                                            IMultiPolygon multiPolygon2 = geometry as IMultiPolygon;
                                            for (int j = 0; j < multiPolygon2.GeometryCount; j++)
                                            {
                                                IPolygon polygon = multiPolygon2.GetPolygon(j);
                                                if (polygon != null)
                                                {
                                                    multiPolygon.AddPolygon(polygon);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            if (multiPolygon.GeometryCount < 1)
                            {
                                XtraMessageBox.Show("无可用范围数据!");
                                result = null;
                            }
                            else
                            {
                                result = multiPolygon;
                            }
                        }
                    }
                    else
                    {
                        XtraMessageBox.Show("打开shp文件失败!");
                        result = null;
                    }
                }
                else
                {
                    XtraMessageBox.Show("名称为空");
                    result = null;
                }
            }
            catch (System.Runtime.InteropServices.COMException ex)
            {
                XtraMessageBox.Show(ex.Message);
                result = null;
            }
            finally
            {
                if (dataSource != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(dataSource);
                    dataSource = null;
                }
                if (featureDataSet != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(featureDataSet);
                    featureDataSet = null;
                }
                if (featureClass != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(featureClass);
                    featureClass = null;
                }
                if (fdeCursor != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(fdeCursor);
                    fdeCursor = null;
                }
            }
            return result;
        }
        private void SetMakePolygonMode()
        {
            DF3DApplication app = DF3DApplication.Application;
            if (app == null || app.Current3DMapControl == null) return;
            this.textEdit_FilePath.Text = string.Empty;
            this.DeleteRenderGeo(ref this._renderGeo);
            app.Current3DMapControl.HighlightHelper.SetRegion(null);
            IGeometryFactory geometryFactory = new GeometryFactoryClass();
            if (this.radioGroup1.SelectedIndex == 1)
            {
                base.WindowState = System.Windows.Forms.FormWindowState.Minimized;
                this.textEdit_FilePath.Enabled = false;
                this.sbtn_BrowseFile.Enabled = false;
                IPolygon polygon = geometryFactory.CreateGeometry(gviGeometryType.gviGeometryPolygon, gviVertexAttribute.gviVertexAttributeZ) as IPolygon;
                polygon.SpatialCRS = CommonUtils.Instance().GetCurrentFeatureDataset().SpatialReference;
                ISurfaceSymbol surfaceSymbol = new SurfaceSymbolClass();
                surfaceSymbol.Color = 1442840448u;
                surfaceSymbol.BoundarySymbol = new CurveSymbolClass
                {
                    Color = 1442840448u
                };
                this._renderGeo = app.Current3DMapControl.ObjectManager.CreateRenderPolygon(polygon, surfaceSymbol, app.Current3DMapControl.ProjectTree.RootID);
                GeometryEdit.Instance().GeometryEditStart(this._renderGeo);
            }
            else
            {
                if (this.radioGroup1.SelectedIndex == 0)
                {
                    this.textEdit_FilePath.Enabled = true;
                    this.sbtn_BrowseFile.Enabled = true;
                }
            }
        }
        private void DeleteRenderGeo(ref IRenderGeometry renderGeo)
        {
            DF3DApplication app = DF3DApplication.Application;
            if (app == null || app.Current3DMapControl == null) return;
            if (renderGeo != null && System.Runtime.InteropServices.Marshal.IsComObject(renderGeo))
            {
                app.Current3DMapControl.ObjectManager.DeleteObject(renderGeo.Guid);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(renderGeo);
                renderGeo = null;
            }
        }
        private void SplitDlg_FormClosed(object sender, System.Windows.Forms.FormClosedEventArgs e)
        {
            DF3DApplication app = DF3DApplication.Application;
            if (app == null || app.Current3DMapControl == null) return;
            this.DeleteRenderGeo(ref this._renderGeo);
            app.Current3DMapControl.HighlightHelper.SetRegion(null);
            //MainFrmService.RibbonControl.Enabled = true;
            //MainFrmService.LayerTreePanel.Enabled = true;
            //MainFrmService.HideContainerBottom.Enabled = true;
            //MainFrmService.RightPanelContainer.Enabled = true;
            GeometryEdit.Instance().SpatialQueryEditStopEvent -= new SpatialQueryEditStopHandle(this.SpatialQueryEditStopEvent);
            app.Current3DMapControl.InteractMode = this._interactaMode;
        }
        private void SpatialQueryEditStopEvent(bool bFinish)
        {
            base.WindowState = System.Windows.Forms.FormWindowState.Normal;
            if (!bFinish)
            {
                this.DeleteRenderGeo(ref this._renderGeo);
            }
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }
        private void InitializeComponent()
        {
            this.layoutControl1 = new LayoutControl();
            this.sbtn_Reset = new SimpleButton();
            this.sbtn_Cancel = new SimpleButton();
            this.sbtn_Split = new SimpleButton();
            this.radioGroup1 = new RadioGroup();
            this.sbtn_BrowseFile = new SimpleButton();
            this.textEdit_FilePath = new TextEdit();
            this.layoutControlGroup1 = new LayoutControlGroup();
            this.emptySpaceItem1 = new EmptySpaceItem();
            this.layoutControlGroup2 = new LayoutControlGroup();
            this.layoutControlItem1 = new LayoutControlItem();
            this.layoutControlItem2 = new LayoutControlItem();
            this.layoutControlItem3 = new LayoutControlItem();
            this.layoutControlItem6 = new LayoutControlItem();
            this.emptySpaceItem2 = new EmptySpaceItem();
            this.emptySpaceItem3 = new EmptySpaceItem();
            this.layoutControlItem4 = new LayoutControlItem();
            this.layoutControlItem5 = new LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)this.layoutControl1).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)this.radioGroup1.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)this.textEdit_FilePath.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)this.layoutControlGroup1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)this.emptySpaceItem1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)this.layoutControlGroup2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)this.layoutControlItem1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)this.layoutControlItem2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)this.layoutControlItem3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)this.layoutControlItem6).BeginInit();
            ((System.ComponentModel.ISupportInitialize)this.emptySpaceItem2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)this.emptySpaceItem3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)this.layoutControlItem4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)this.layoutControlItem5).BeginInit();
            base.SuspendLayout();
            this.layoutControl1.Controls.Add(this.sbtn_Reset);
            this.layoutControl1.Controls.Add(this.sbtn_Cancel);
            this.layoutControl1.Controls.Add(this.sbtn_Split);
            this.layoutControl1.Controls.Add(this.radioGroup1);
            this.layoutControl1.Controls.Add(this.sbtn_BrowseFile);
            this.layoutControl1.Controls.Add(this.textEdit_FilePath);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle?(new System.Drawing.Rectangle(392, 79, 250, 350));
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(358, 147);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            this.sbtn_Reset.Location = new System.Drawing.Point(304, 39);
            this.sbtn_Reset.Name = "sbtn_Reset";
            this.sbtn_Reset.Size = new System.Drawing.Size(35, 22);
            this.sbtn_Reset.StyleController = this.layoutControl1;
            this.sbtn_Reset.TabIndex = 9;
            this.sbtn_Reset.Text = "重置";
            this.sbtn_Reset.Click += new System.EventHandler(this.sbtn_Reset_Click);
            this.sbtn_Cancel.Location = new System.Drawing.Point(197, 106);
            this.sbtn_Cancel.Name = "sbtn_Cancel";
            this.sbtn_Cancel.Size = new System.Drawing.Size(97, 22);
            this.sbtn_Cancel.StyleController = this.layoutControl1;
            this.sbtn_Cancel.TabIndex = 8;
            this.sbtn_Cancel.Text = "取消";
            this.sbtn_Cancel.Click += new System.EventHandler(this.sbtn_Cancel_Click);
            this.sbtn_Split.Location = new System.Drawing.Point(63, 106);
            this.sbtn_Split.Name = "sbtn_Split";
            this.sbtn_Split.Size = new System.Drawing.Size(85, 22);
            this.sbtn_Split.StyleController = this.layoutControl1;
            this.sbtn_Split.TabIndex = 7;
            this.sbtn_Split.Text = "拆分";
            this.sbtn_Split.Click += new System.EventHandler(this.sbtn_Split_Click);
            this.radioGroup1.Location = new System.Drawing.Point(19, 39);
            this.radioGroup1.Name = "radioGroup1";
            this.radioGroup1.Properties.Items.AddRange(new RadioGroupItem[]
			{
				new RadioGroupItem(null, "文件导入"), 
				new RadioGroupItem(null, "绘制")
			});
            this.radioGroup1.Size = new System.Drawing.Size(281, 25);
            this.radioGroup1.StyleController = this.layoutControl1;
            this.radioGroup1.TabIndex = 6;
            this.radioGroup1.SelectedIndexChanged += new System.EventHandler(this.radioGroup1_SelectedIndexChanged);
            this.sbtn_BrowseFile.Location = new System.Drawing.Point(304, 68);
            this.sbtn_BrowseFile.Name = "sbtn_BrowseFile";
            this.sbtn_BrowseFile.Size = new System.Drawing.Size(35, 22);
            this.sbtn_BrowseFile.StyleController = this.layoutControl1;
            this.sbtn_BrowseFile.TabIndex = 5;
            this.sbtn_BrowseFile.Text = "...";
            this.sbtn_BrowseFile.Click += new System.EventHandler(this.sbtn_BrowseFile_Click);
            this.textEdit_FilePath.Location = new System.Drawing.Point(19, 68);
            this.textEdit_FilePath.Name = "textEdit_FilePath";
            this.textEdit_FilePath.Properties.ReadOnly = true;
            this.textEdit_FilePath.Size = new System.Drawing.Size(281, 20);
            this.textEdit_FilePath.StyleController = this.layoutControl1;
            this.textEdit_FilePath.TabIndex = 4;
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new BaseLayoutItem[]
			{
				this.emptySpaceItem1, 
				this.layoutControlGroup2, 
				this.emptySpaceItem2, 
				this.emptySpaceItem3, 
				this.layoutControlItem4, 
				this.layoutControlItem5
			});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlGroup1.Size = new System.Drawing.Size(358, 147);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 99);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(56, 38);
            this.emptySpaceItem1.Text = "emptySpaceItem1";
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlGroup2.CustomizationFormText = "范围多边形";
            this.layoutControlGroup2.Items.AddRange(new BaseLayoutItem[]
			{
				this.layoutControlItem1, 
				this.layoutControlItem2, 
				this.layoutControlItem3, 
				this.layoutControlItem6
			});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Size = new System.Drawing.Size(348, 99);
            this.layoutControlGroup2.Text = "范围多边形";
            this.layoutControlItem1.Control = this.textEdit_FilePath;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 29);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(285, 26);
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            this.layoutControlItem2.Control = this.sbtn_BrowseFile;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(285, 29);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(39, 26);
            this.layoutControlItem2.Text = "layoutControlItem2";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            this.layoutControlItem3.Control = this.radioGroup1;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(285, 29);
            this.layoutControlItem3.Text = "layoutControlItem3";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            this.layoutControlItem6.Control = this.sbtn_Reset;
            this.layoutControlItem6.CustomizationFormText = "layoutControlItem6";
            this.layoutControlItem6.Location = new System.Drawing.Point(285, 0);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(39, 29);
            this.layoutControlItem6.Text = "layoutControlItem6";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextToControlDistance = 0;
            this.layoutControlItem6.TextVisible = false;
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.CustomizationFormText = "emptySpaceItem2";
            this.emptySpaceItem2.Location = new System.Drawing.Point(291, 99);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(57, 38);
            this.emptySpaceItem2.Text = "emptySpaceItem2";
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            this.emptySpaceItem3.AllowHotTrack = false;
            this.emptySpaceItem3.CustomizationFormText = "emptySpaceItem3";
            this.emptySpaceItem3.Location = new System.Drawing.Point(145, 99);
            this.emptySpaceItem3.Name = "emptySpaceItem3";
            this.emptySpaceItem3.Size = new System.Drawing.Size(45, 38);
            this.emptySpaceItem3.Text = "emptySpaceItem3";
            this.emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.Control = this.sbtn_Split;
            this.layoutControlItem4.CustomizationFormText = "layoutControlItem4";
            this.layoutControlItem4.Location = new System.Drawing.Point(56, 99);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(89, 38);
            this.layoutControlItem4.Text = "layoutControlItem4";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextToControlDistance = 0;
            this.layoutControlItem4.TextVisible = false;
            this.layoutControlItem5.Control = this.sbtn_Cancel;
            this.layoutControlItem5.CustomizationFormText = "layoutControlItem5";
            this.layoutControlItem5.Location = new System.Drawing.Point(190, 99);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(101, 38);
            this.layoutControlItem5.Text = "layoutControlItem5";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextToControlDistance = 0;
            this.layoutControlItem5.TextVisible = false;
            base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 14f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.ClientSize = new System.Drawing.Size(358, 147);
            base.Controls.Add(this.layoutControl1);
            base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            base.Name = "SplitDlg";
            base.ShowInTaskbar = false;
            base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "拆分";
            base.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SplitDlg_FormClosed);
            ((System.ComponentModel.ISupportInitialize)this.layoutControl1).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)this.radioGroup1.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)this.textEdit_FilePath.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)this.layoutControlGroup1).EndInit();
            ((System.ComponentModel.ISupportInitialize)this.emptySpaceItem1).EndInit();
            ((System.ComponentModel.ISupportInitialize)this.layoutControlGroup2).EndInit();
            ((System.ComponentModel.ISupportInitialize)this.layoutControlItem1).EndInit();
            ((System.ComponentModel.ISupportInitialize)this.layoutControlItem2).EndInit();
            ((System.ComponentModel.ISupportInitialize)this.layoutControlItem3).EndInit();
            ((System.ComponentModel.ISupportInitialize)this.layoutControlItem6).EndInit();
            ((System.ComponentModel.ISupportInitialize)this.emptySpaceItem2).EndInit();
            ((System.ComponentModel.ISupportInitialize)this.emptySpaceItem3).EndInit();
            ((System.ComponentModel.ISupportInitialize)this.layoutControlItem4).EndInit();
            ((System.ComponentModel.ISupportInitialize)this.layoutControlItem5).EndInit();
            base.ResumeLayout(false);
        }
    }
}
