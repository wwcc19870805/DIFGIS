using System;
using System.Collections.Generic;
using System.Linq;
using Gvitech.CityMaker.FdeCore;
using Gvitech.CityMaker.FdeGeometry;
using ICSharpCode.Core;
using DF3DDraw;
using DF3DControl.Command;
using DF3DControl.Base;
using DF3DData.Class;
using DF3DEdit.Class;
using DF3DEdit.Service;

namespace DF3DEdit.Command
{
    public class CmdCircleSelect : AbstractMap3DCommand
    {
        private DrawTool _drawTool;

        public override void Run(object sender, System.EventArgs e)
        {
            Map3DCommandManager.Push(this);
            this._drawTool = DrawToolService.Instance.CreateDrawTool(DrawType.Circle);
            if (this._drawTool != null)
            {
                this._drawTool.OnStartDraw += new OnStartDraw(this.OnStartDraw);
                this._drawTool.OnFinishedDraw += new OnFinishedDraw(this.OnFinishedDraw);
                this._drawTool.Start();
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
            Map3DCommandManager.Pop();
            //RenderControlEditServices.Instance().StopGeometryEdit(true);
            //FDECommand fDECommand = new FDECommand(true, false);
            //SelectCollection.Instance().Clear();
            //SelectCollection.Instance().ClearRowBuffers();
            //RenderControlEditServices.Instance().SetEditorPosition(null);
            //fDECommand.SetSelectionMap();
        }

        private void OnStartDraw()
        {
            if (this._drawTool != null)
            {
                Clear();
            }
        }

        public void Clear()
        {
            if (this._drawTool != null)
            {
                this._drawTool.Close();
            }
        }

        private void OnFinishedDraw()
        {
            DF3DApplication app = DF3DApplication.Application;
            if (app == null || app.Current3DMapControl == null) return;
            if (this._drawTool != null && this._drawTool.GeoType == DrawType.Circle && this._drawTool.GetGeo() != null)
            {
                //FDECommand fDECommand = new FDECommand(true, false);
                SpatialQuery();
                //fDECommand.SetSelectionMap();
                //CommandManagerServices.Instance().CallCommand(fDECommand);
                app.Workbench.UpdateMenu();
            }
        }

        private void SpatialQuery()
        {
            DF3DApplication app = DF3DApplication.Application;
            if (app == null || app.Current3DMapControl == null) return;

            try
            {
                HashMap hashMap = new HashMap();
                IRowBuffer buffer = null;
                IFdeCursor cursor = null;
                ISpatialFilter filter = null;
                IGeometry geo2D = this._drawTool.GetGeo();
                if (geo2D != null && geo2D.GeometryType == gviGeometryType.gviGeometryPolygon)
                {
                    IPolygon polygon = geo2D as IPolygon;
                    if (polygon != null)
                    {
                        DF3DFeatureClass featureClassInfo = CommonUtils.Instance().CurEditLayer;
                        if (featureClassInfo != null)
                        {
                            IFeatureClass featureClass = featureClassInfo.GetFeatureClass();
                            if (featureClass != null)
                            {
                                string typeName = featureClassInfo.GetFacilityClassName();
                                if (typeName == "PipeLine" || typeName == "PipeNode" || typeName == "PipeBuild" || typeName == "PipeBuild1")
                                {
                                    filter = new SpatialFilterClass
                                    {
                                        GeometryField = "Shape",
                                        SpatialRel = gviSpatialRel.gviSpatialRelIntersects,
                                        Geometry = polygon.Clone2(gviVertexAttribute.gviVertexAttributeNone)
                                    };
                                }
                                else
                                {
                                    filter = new SpatialFilterClass
                                    {
                                        Geometry = polygon,
                                        GeometryField = "Geometry",
                                        SpatialRel = gviSpatialRel.gviSpatialRelEnvelope
                                    };
                                }
                                filter.SubFields = featureClass.FidFieldName;
                                cursor = featureClass.Search(filter, true);
                                while ((buffer = cursor.NextRow()) != null)
                                {
                                    int featureId = int.Parse(buffer.GetValue(0).ToString());
                                    if (hashMap.Contains(featureClassInfo))
                                    {
                                        System.Collections.Generic.List<int> list = hashMap[featureClassInfo] as System.Collections.Generic.List<int>;
                                        if (!list.Contains(featureId))
                                        {
                                            list.Add(featureId);
                                        }
                                    }
                                    else
                                    {
                                        System.Collections.Generic.List<int> list2 = new System.Collections.Generic.List<int>();
                                        if (!list2.Contains(featureId))
                                        {
                                            list2.Add(featureId);
                                        }
                                        hashMap[featureClassInfo] = list2;
                                    }
                                }
                            }
                        }
                    }
                    SelectCollection.Instance().UpdateSelection(hashMap);
                    RenderControlEditServices.Instance().SetEditorPosition(SelectCollection.Instance().FcRowBuffersMap);
                    this.Clear();
                }
                if (buffer != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(buffer);
                }
                if (cursor != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(cursor);
                }
                if (filter != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(filter);
                }
            }
            catch (Exception ex)
            {
                LoggingService.Error(ex.Message);
            }
        }

    }
}
