using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DF3DControl.Command;
using DF3DDraw;
using DF3DData.Class;
using Gvitech.CityMaker.FdeCore;
using DFDataConfig.Class;
using Gvitech.CityMaker.FdeGeometry;
using System.IO;
using System.Windows.Forms;
using DFCommon.Class;
using DF3DControl.Base;
using Gvitech.CityMaker.RenderControl;
using DFWinForms.Class;
using DFDataConfig.Logic;

namespace DF3DPipe.Analysis.Command
{
    class CmdFireHydrantSearch : AbstractMap3DCommand
    {
        private DrawTool _drawTool;
        private List<Guid> _listRGuid = new List<Guid>();
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
            if (this._drawTool != null && this._drawTool.GeoType == DrawType.Circle && this._drawTool.GetGeo() != null)
            {
                CircleQuery();
            }
        }

        public void Clear()
        {
            if (this._drawTool != null)
            {
                this._drawTool.Close();
            }
            DF3DApplication app = DF3DApplication.Application;
            if (app == null || app.Current3DMapControl == null) return;
            foreach (Guid guid in this._listRGuid)
            {
                app.Current3DMapControl.ObjectManager.DeleteObject(guid);
            }
            this._listRGuid.Clear();

        }

        private void CircleQuery()
        {
            DF3DApplication app = DF3DApplication.Application;
            if (app == null || app.Current3DMapControl == null) return;
            WaitForm.Start("正在搜索...", "请稍后");
            try
            {
                FacilityClass fac = FacilityClassManager.Instance.GetFacilityClassByName("PipeNode");
                if (fac == null) return;
                DFDataConfig.Class.FieldInfo field = fac.GetFieldInfoBySystemName("Additional");
                if (field == null) return;
                ISpatialFilter filter = new SpatialFilterClass();
                filter.SubFields = "oid,Geometry,FootPrint";
                string configFH = Config.GetConfigValue("FireHydrantName");
                string fireHydrantName = "'" + configFH + "'";
                filter.WhereClause = field.Name + " in (" + fireHydrantName + ")";
                filter.Geometry = this._drawTool.GetGeo().Clone2(gviVertexAttribute.gviVertexAttributeNone);
                filter.GeometryField = "FootPrint";
                filter.SpatialRel = gviSpatialRel.gviSpatialRelContains;

                List<DF3DFeatureClass> list = DF3DFeatureClassManager.Instance.GetFeatureClassByFacilityClassName("PipeNode");
                if (list == null) return;

                foreach (DF3DFeatureClass dffc in list)
                {
                    IFeatureClass fc = dffc.GetFeatureClass();

                    if (fc == null) continue;
                    MajorClass mc = LogicDataStructureManage3D.Instance.GetMajorClassByDFFeatureClassID(fc.GuidString);
                    if (mc != null && mc.Name != "GS") continue;

                    IFdeCursor cursor = null;
                    IRowBuffer row = null;
                    try
                    {
                        cursor = fc.Search(filter, false);
                        while ((row = cursor.NextRow()) != null)
                        {
                            if (!row.IsNull(1))
                            {
                                IGeometry geo = row.GetValue(1) as IGeometry;
                                switch (geo.GeometryType)
                                {
                                    case gviGeometryType.gviGeometryPoint:
                                        IPoint pt = geo as IPoint;
                                        IPOI poi = (new GeometryFactoryClass()).CreateGeometry(gviGeometryType.gviGeometryPOI, gviVertexAttribute.gviVertexAttributeZ) as IPOI;
                                        poi.X = pt.X;
                                        poi.Y = pt.Y;
                                        poi.Z = pt.Z + 2;
                                        poi.ImageName = Path.Combine(Application.StartupPath, "..\\Resource\\Images\\POI\\fireHydrant.png");
                                        poi.Size = SystemInfo.Instance.SymbolSize;
                                        poi.ShowName = false;
                                        IRenderPOI rpoi = app.Current3DMapControl.ObjectManager.CreateRenderPOI(poi);
                                        this._listRGuid.Add(rpoi.Guid);
                                        continue;
                                    case gviGeometryType.gviGeometryModelPoint:
                                        IModelPoint pt1 = geo as IModelPoint;
                                        IPOI poi1 = (new GeometryFactoryClass()).CreateGeometry(gviGeometryType.gviGeometryPOI, gviVertexAttribute.gviVertexAttributeZ) as IPOI;
                                        poi1.X = pt1.X;
                                        poi1.Y = pt1.Y;
                                        poi1.Z = pt1.Z + 2;
                                        poi1.ImageName = Path.Combine(Application.StartupPath, "..\\Resource\\Images\\POI\\fireHydrant.png");
                                        poi1.Size = SystemInfo.Instance.SymbolSize;
                                        poi1.ShowName = false;
                                        IRenderPOI rpoi1 = app.Current3DMapControl.ObjectManager.CreateRenderPOI(poi1);
                                        this._listRGuid.Add(rpoi1.Guid);
                                        continue;
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
            catch (Exception ex) { }
            finally
            {
                WaitForm.Stop();
            }
        }

    }
}
