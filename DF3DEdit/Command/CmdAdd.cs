using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DF3DControl.Command;
using DF3DData.Class;
using DF3DEdit.Service;
using Gvitech.CityMaker.RenderControl;
using DF3DDraw;
using Gvitech.CityMaker.FdeCore;
using DF3DControl.Base;
using DF3DEdit.Class;
using DF3DEdit.Interface;
using Gvitech.CityMaker.Controls;
using Gvitech.CityMaker.FdeGeometry;
using DF3DEdit.Frm;
using System.IO;
using Gvitech.CityMaker.Resource;
using Gvitech.CityMaker.Common;
using System.Windows.Forms;
using DF3DPipeCreateTool.Class;
using Gvitech.CityMaker.Math;

namespace DF3DEdit.Command
{
    public class CmdAdd : AbstractMap3DCommand
    {
        private DrawTool _drawTool;
        private gviGeometryColumnType _geoType;
        private string _facType;
        private string _strGeometryFieldName;
        private System.DateTime time;
        private DF3DApplication app;
        private IGeometryFactory _geoFact;
        public override void Run(object sender, EventArgs e)
        {
            try
            {
                app = DF3DApplication.Application;
                if (app == null || app.Current3DMapControl == null) return;
                this._geoFact = new GeometryFactory();
                Map3DCommandManager.Push(this);
                DF3DFeatureClass dffc = CommonUtils.Instance().CurEditLayer;
                if (dffc != null)
                {
                    this._facType = dffc.GetFacilityClassName();
                    _strGeometryFieldName = "";
                    if (this._facType == "PipeLine" || this._facType == "PipeNode" || this._facType == "PipeBuild" || this._facType == "PipeBuild1")
                    {
                        _strGeometryFieldName = "Shape";
                    }
                    else
                    {
                        IFeatureLayer fl = dffc.GetFeatureLayer();
                        if (fl != null)
                        {
                            _strGeometryFieldName = fl.GeometryFieldName;
                        }
                    }
                    if (!string.IsNullOrEmpty(_strGeometryFieldName))
                    {
                        IFeatureClass fc = dffc.GetFeatureClass();
                        if (fc != null)
                        {
                            IFieldInfoCollection fiCol = fc.GetFields();
                            int index = fiCol.IndexOf(_strGeometryFieldName);
                            if (index != -1)
                            {
                                IFieldInfo fi = fiCol.Get(index);
                                if (fi.GeometryDef != null)
                                {
                                    this._geoType = fi.GeometryDef.GeometryColumnType;
                                }
                            }
                        }
                        switch (this._geoType)
                        {
                            case gviGeometryColumnType.gviGeometryColumnModelPoint:
                                //添加方式：1、鼠标添加；2、文件添加
                                FrmInsertModel dlg = new FrmInsertModel();
                                if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                                {
                                    string filePath = dlg.FilePath;
                                    string ext = Path.GetExtension(filePath);
                                    if (File.Exists(filePath))
                                    {
                                        int insertType = dlg.InsertType;
                                        if (insertType == 0 && ext.ToLower() == ".osg")
                                        {
                                            this._drawTool = DrawToolService.Instance.CreateDrawTool(DrawType._3DModel);
                                            if (this._drawTool != null)
                                            {
                                                (this._drawTool as Draw3DModel).Set3DModelFilePath(filePath);
                                                this._drawTool.OnStartDraw += new OnStartDraw(this.OnStartDraw);
                                                this._drawTool.OnFinishedDraw += new OnFinishedDraw(this.OnFinishedDraw);
                                                this._drawTool.Start();
                                            }
                                        }
                                        else if (insertType == 1 && ext.ToLower() == ".xml")
                                        {

                                        }
                                    }
                                }
                                break;
                            case gviGeometryColumnType.gviGeometryColumnPoint:
                                if (this._facType == "PipeNode")
                                {// 添加附属设施或者管点

                                }
                                else
                                {// 添加点
                                    this._drawTool = DrawToolService.Instance.CreateDrawTool(DrawType.Point);
                                    if (this._drawTool != null)
                                    {
                                        this._drawTool.OnStartDraw += new OnStartDraw(this.OnStartDraw);
                                        this._drawTool.OnFinishedDraw += new OnFinishedDraw(this.OnFinishedDraw);
                                        this._drawTool.Start();
                                    }
                                }
                                break;
                            case gviGeometryColumnType.gviGeometryColumnPolyline:
                                if (this._facType == "PipeLine" || this._facType == "PipeBuild" || this._facType == "PipeBuild1")
                                {// 选择管线或辅助样式

                                }
                                else
                                {// 添加线
                                    this._drawTool = DrawToolService.Instance.CreateDrawTool(DrawType.Polyline);
                                    if (this._drawTool != null)
                                    {
                                        this._drawTool.OnStartDraw += new OnStartDraw(this.OnStartDraw);
                                        this._drawTool.OnFinishedDraw += new OnFinishedDraw(this.OnFinishedDraw);
                                        this._drawTool.Start();
                                    }
                                }
                                break;
                            case gviGeometryColumnType.gviGeometryColumnPolygon:
                                if (this._facType == "PipeBuild" || this._facType == "PipeBuild1")
                                {// 选择管线辅助样式

                                }
                                else
                                {// 添加面
                                    this._drawTool = DrawToolService.Instance.CreateDrawTool(DrawType.Polygon);
                                    if (this._drawTool != null)
                                    {
                                        this._drawTool.OnStartDraw += new OnStartDraw(this.OnStartDraw);
                                        this._drawTool.OnFinishedDraw += new OnFinishedDraw(this.OnFinishedDraw);
                                        this._drawTool.Start();
                                    }
                                }
                                break;
                            default:
                                return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
            }
        }

        public override void RestoreEnv()
        {
            Clear();
            if (this._drawTool != null)
            {
                this._drawTool.OnStartDraw -= new OnStartDraw(this.OnStartDraw);
                this._drawTool.OnFinishedDraw -= new OnFinishedDraw(this.OnFinishedDraw);
                this._drawTool.Close();
                this._drawTool.End();
            }
            //Map3DCommandManager.Pop();
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
                //添加到要素类中
                if (this._geoType == gviGeometryColumnType.gviGeometryColumnPoint)
                {
                    AddGeometry();
                    UpdateDatabase();
                }
                else if (this._geoType == gviGeometryColumnType.gviGeometryColumnPoint)
                {
                    if (this._facType != "PipeNode")
                    {
                        AddGeometry();
                        UpdateDatabase();
                    }
                    else
                    {
                        if (this._facType == "PipeNode")
                        {

                        }
                    }
                }
                else if (this._geoType == gviGeometryColumnType.gviGeometryColumnPolyline)
                {
                    if (this._facType != "PipeLine" && this._facType != "PipeBuild" && this._facType != "PipeBuild1")
                    {
                        AddGeometry();
                        UpdateDatabase();
                    }
                    else
                    {
                        if (this._facType == "PipeLine")
                        {

                        }
                        else if (this._facType == "PipeBuild")
                        {

                        }
                        else if (this._facType == "PipeBuild1")
                        {

                        }
                    }
                }
                else if (this._geoType == gviGeometryColumnType.gviGeometryColumnPolygon)
                {
                    if (this._facType != "PipeBuild" && this._facType != "PipeBuild1")
                    {
                        AddGeometry();
                        UpdateDatabase();
                    }
                    else
                    {
                        if (this._facType == "PipeBuild")
                        {

                        }
                        else if (this._facType == "PipeBuild1")
                        {

                        }
                    }
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

        private Dictionary<DF3DFeatureClass, IRowBufferCollection> beforeRowBufferMap = new Dictionary<DF3DFeatureClass, IRowBufferCollection>();
        private void AddGeometry()
        {
            try
            {
                this.beforeRowBufferMap.Clear();
                SelectCollection.Instance().Clear();
                DF3DFeatureClass featureClassInfo = CommonUtils.Instance().CurEditLayer;
                if (featureClassInfo == null) return;
                IFeatureClass fc = featureClassInfo.GetFeatureClass();
                if (fc == null) return;
                IFieldInfoCollection fields = fc.GetFields();
                int indexGeo = fields.IndexOf(this._strGeometryFieldName);
                if (indexGeo == -1) return;

                IGeometry geo = this._drawTool.GetGeo();
                IGeometry geoOut = null;
                switch (geo.GeometryType)
                {
                    case gviGeometryType.gviGeometryModelPoint:
                        geoOut = geo;
                        break;
                    case gviGeometryType.gviGeometryPoint:
                        IPoint pt = geo as IPoint;
                        IPoint pt1 = pt.Clone2(gviVertexAttribute.gviVertexAttributeZM) as IPoint;
                        pt1.SetCoords(pt.X, pt.Y, pt.Z, 0, 0);
                        geoOut = pt1;
                        break;
                    case gviGeometryType.gviGeometryPolyline:
                        IPolyline line = geo as IPolyline;
                        IPolyline line1 = this._geoFact.CreateGeometry(gviGeometryType.gviGeometryPolyline, gviVertexAttribute.gviVertexAttributeZM) as IPolyline;
                        for (int i = 0; i < line.PointCount; i++)
                        {
                            IPoint ptGet = line.GetPoint(i);
                            IPoint pttemp = ptGet.Clone2(gviVertexAttribute.gviVertexAttributeZM) as IPoint;
                            pttemp.SetCoords(ptGet.X, ptGet.Y, ptGet.Z, 0, 0);
                            line1.AppendPoint(pttemp);
                        }
                        geoOut = line1;
                        break;
                    case gviGeometryType.gviGeometryPolygon:
                        IPolygon polygon = geo as IPolygon;
                        IPolygon polygon1 = this._geoFact.CreateGeometry(gviGeometryType.gviGeometryPolygon, gviVertexAttribute.gviVertexAttributeZM) as IPolygon;
                        for (int i = 0; i < polygon.ExteriorRing.PointCount; i++)
                        {
                            IPoint ptGet = polygon.ExteriorRing.GetPoint(i);
                            IPoint pttemp = ptGet.Clone2(gviVertexAttribute.gviVertexAttributeZM) as IPoint;
                            pttemp.SetCoords(ptGet.X, ptGet.Y, ptGet.Z, 0, 0);
                            polygon1.ExteriorRing.AppendPoint(pttemp);
                        }
                        geoOut = polygon1;
                        break;
                }
                if (geoOut == null) return;
                if (geo.GeometryType == gviGeometryType.gviGeometryModelPoint)
                {
                    //导入模型到数据库中
                    string mn = BitConverter.ToString(ObjectIdGenerator.Generate()).Replace("-", string.Empty).ToLowerInvariant();
                    IModelPoint mp = geoOut as IModelPoint;
                    bool bRes = this.ImportOsg(mn, mp.ModelName);
                    if(!bRes) return;
                    mp.ModelName = mn;
                    geoOut = mp;
                }
                IRowBufferCollection rowCol = new RowBufferCollection();
                IRowBufferFactory fac = new RowBufferFactory();
                IRowBuffer row = fac.CreateRowBuffer(fields);
                row.SetValue(indexGeo, geoOut);                
                rowCol.Add(row);
                beforeRowBufferMap[featureClassInfo] = rowCol;
            }
            catch (Exception ex)
            {
            }
        }

        private void UpdateDatabase()
        {
            DF3DFeatureClass featureClassInfo =  CommonUtils.Instance().CurEditLayer;
            if(featureClassInfo == null) return;
            IFeatureClass fc = featureClassInfo.GetFeatureClass();
            if(fc == null) return;
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

        private bool ImportOsg(string modelName, string filePath)
        {
            try
            {
                IImage property = null;
                IMatrix mat = null;
                IPropertySet images = null;
                IModel smodel = null;
                IModel fmodel = null;
                bool bRes = this.OpenOsgModel(filePath, out fmodel, out smodel, out images, out mat);
                if (!bRes) return false;
                DF3DFeatureClass dffc = CommonUtils.Instance().CurEditLayer;
                if (dffc != null)
                {
                    IFeatureClass fc = dffc.GetFeatureClass();
                    if (fc != null)
                    {
                        IResourceManager resManager = fc.FeatureDataSet as IResourceManager;
                        if ((images != null) && (images.Count > 0))
                        {
                            foreach (string str in images.GetAllKeys())
                            {
                                if (!resManager.ImageExist(str))
                                {
                                    property = images.GetProperty(str) as IImage;
                                    bRes = resManager.AddImage(str, property);
                                    if (!bRes) return false;
                                }
                            }
                        }
                        bRes = resManager.AddModel(modelName, fmodel, smodel);
                        if (!bRes) return false;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private bool OpenOsgModel(string osgPath, out IModel fmodel, out IModel smodel, out IPropertySet images, out IMatrix mat)
        {
            fmodel = null;
            smodel = null;
            images = null;
            mat = null;
            IResourceFactory resFactory = new ResourceFactoryClass();
            if ((resFactory == null) || !File.Exists(osgPath))
            {
                return false;
            }
            Dictionary<string, string> dictionary = null;
            IDrawGroup group = null;
            IDrawPrimitive primitive = null;
            IPropertySet set = null;
            string str = "";
            IImage property = null;
            try
            {
                resFactory.CreateModelAndImageFromFileEx(osgPath, out images, out smodel, out fmodel, out mat);
                if ((images != null) && (images.Count > 0))
                {
                    set = new PropertySetClass();
                    dictionary = new Dictionary<string, string>();
                    foreach (string str2 in images.GetAllKeys())
                    {
                        property = images.GetProperty(str2) as IImage;
                        IImage temp = null;
                        string filePath = string.Format(string.Format(@"{0}\..\temp\{1}.png", Application.StartupPath, Guid.NewGuid().ToString()), new object[0]);
                        if (property.WriteFile(filePath))
                        {
                            temp = resFactory.CreateImageFromFile(filePath);
                        }
                        str = BitConverter.ToString(ObjectIdGenerator.Generate()).Replace("-", string.Empty).ToLowerInvariant();
                        dictionary.Add(str2, str);
                        set.SetProperty(str, temp);
                        if (File.Exists(filePath))
                        {
                            File.Delete(filePath);
                        }
                    }
                    images = set;
                }
                if ((fmodel != null) && (fmodel.GroupCount > 0))
                {
                    for (int i = 0; i < fmodel.GroupCount; i++)
                    {
                        group = fmodel.GetGroup(i);
                        if (group != null)
                        {
                            if (!string.IsNullOrEmpty(group.CompleteMapTextureName) && dictionary.ContainsKey(group.CompleteMapTextureName))
                            {
                                group.CompleteMapTextureName = dictionary[group.CompleteMapTextureName];
                            }
                            if (!string.IsNullOrEmpty(group.LightMapTextureName) && dictionary.ContainsKey(group.LightMapTextureName))
                            {
                                group.LightMapTextureName = dictionary[group.LightMapTextureName];
                            }
                            if (group.PrimitiveCount > 0)
                            {
                                for (int j = 0; j < group.PrimitiveCount; j++)
                                {
                                    primitive = group.GetPrimitive(j);
                                    if (((primitive != null) && (primitive.Material != null)) && (!string.IsNullOrEmpty(primitive.Material.TextureName) && dictionary.ContainsKey(primitive.Material.TextureName)))
                                    {
                                        primitive.Material.TextureName = dictionary[primitive.Material.TextureName];
                                    }
                                }
                            }
                        }
                    }
                }
                if ((smodel != null) && (smodel.GroupCount > 0))
                {
                    for (int k = 0; k < smodel.GroupCount; k++)
                    {
                        group = smodel.GetGroup(k);
                        if (group != null)
                        {
                            if (!string.IsNullOrEmpty(group.CompleteMapTextureName) && dictionary.ContainsKey(group.CompleteMapTextureName))
                            {
                                group.CompleteMapTextureName = dictionary[group.CompleteMapTextureName];
                            }
                            if (!string.IsNullOrEmpty(group.LightMapTextureName) && dictionary.ContainsKey(group.LightMapTextureName))
                            {
                                group.LightMapTextureName = dictionary[group.LightMapTextureName];
                            }
                            if (group.PrimitiveCount > 0)
                            {
                                for (int m = 0; m < group.PrimitiveCount; m++)
                                {
                                    primitive = group.GetPrimitive(m);
                                    if (((primitive != null) && (primitive.Material != null)) && (!string.IsNullOrEmpty(primitive.Material.TextureName) && dictionary.ContainsKey(primitive.Material.TextureName)))
                                    {
                                        primitive.Material.TextureName = dictionary[primitive.Material.TextureName];
                                    }
                                }
                            }
                        }
                    }
                }
                return true;
            }
            catch (Exception exception)
            {
                return false;
            }
        }

    }
}
