using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DF3DControl.Command;
using DF3DDraw;
using DF3DControl.Base;
using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.Controls;
using Gvitech.CityMaker.RenderControl;
using DF3DData.Class;
using DFDataConfig.Class;
using DFWinForms.LogicTree;

namespace DF3DScan.Command
{
    public class CmdCreateTerrainHole : AbstractMap3DCommand
    {
        private DrawTool _drawTool;
        private List<Guid> _listRGuids = new List<Guid>();
        private AxRenderControl d3;
        private Dictionary<IBaseLayer, bool> dict;

        public override void Run(object sender, System.EventArgs e)
        {
            if (DF3DApplication.Application == null || DF3DApplication.Application.Current3DMapControl == null) return;
            d3 = DF3DApplication.Application.Current3DMapControl;
            if (!d3.Terrain.IsRegistered) return;
            Map3DCommandManager.Push(this);
            d3.Terrain.DemAvailable = true;
            DF3DApplication.Application.Workbench.UpdateMenu();
            List<DF3DFeatureClass> list = Dictionary3DTable.Instance.GetFeatureClassByFacilityClassName("DX3DMODEL");
            if (list != null)
            {
                dict = new Dictionary<IBaseLayer, bool>();
                foreach (DF3DFeatureClass dffc in list)
                {
                    IBaseLayer bl = dffc.GetTreeLayer();
                    dict[bl] = bl.Visible;
                    if (bl != null)
                    {
                        bl.Visible = false;
                    }
                }
            }
            DF3DApplication.Application.Workbench.SetStatusInfo("请绘制挖洞区域");
            this._drawTool = DrawToolService.Instance.CreateDrawTool(DrawType.Polygon);
            if (this._drawTool != null)
            {
                this._drawTool.OnStartDraw += new OnStartDraw(this.OnStartDraw);
                this._drawTool.OnFinishedDraw += new OnFinishedDraw(this.OnFinishedDraw);
                this._drawTool.Start();
            }
        }

        public override void RestoreEnv()
        {
            if (DF3DApplication.Application == null || DF3DApplication.Application.Current3DMapControl == null) return;
            DF3DApplication.Application.Workbench.SetStatusInfo("");
            if (dict != null)
            {
                foreach (KeyValuePair<IBaseLayer, bool> kv in dict)
                {
                    kv.Key.Visible = kv.Value;
                }
            }
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

        private void Clear()
        {
            if (this._drawTool != null)
            {
                this._drawTool.Close();
            }
            DF3DApplication app = DF3DApplication.Application;
            if (app == null || app.Current3DMapControl == null) return;
            foreach (Guid g in this._listRGuids)
            {
                app.Current3DMapControl.ObjectManager.DeleteObject(g);
            }
            this._listRGuids.Clear();
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
            if (this._drawTool != null && this._drawTool.GeoType == DrawType.Polygon && this._drawTool.GetGeo() != null)
            {
                CreateTerrainHole();
            }
        }

        private void CreateTerrainHole()
        {
            try
            {
                IGeometry geo = this._drawTool.GetGeo();
                if (geo == null) return;
                IPolygon region = geo as IPolygon;
                region.Close();

                IPolygon region2 = geo.Clone2(gviVertexAttribute.gviVertexAttributeNone) as IPolygon;
                double refheight = d3.Terrain.GetElevation(region2.Centroid.X, region2.Centroid.Y, gviGetElevationType.gviGetElevationFromMemory) - 30;

                ITerrainHole terrainHole = d3.ObjectManager.CreateTerrainHole(region, d3.ProjectTree.RootID);
                this._listRGuids.Add(terrainHole.Guid);

                IPolygon bottom = region.Clone() as IPolygon;
                for (int i = 0; i < bottom.ExteriorRing.PointCount; i++)
                {
                    IPoint pointValue = bottom.ExteriorRing.GetPoint(i);
                    pointValue.Z = refheight;
                    bottom.ExteriorRing.UpdatePoint(i, pointValue);
                }
                ICurveSymbol cs = new CurveSymbol();
                cs.Color = 0x00ffffff;
                ISurfaceSymbol bottomSS = new SurfaceSymbol();
                bottomSS.ImageName = System.Windows.Forms.Application.StartupPath + "\\..\\Resource\\Images\\TerrainHole\\TextureBottom.jpg";
                bottomSS.RepeatLengthU = 5;
                bottomSS.RepeatLengthV = 5;
                bottomSS.EnableLight = true;
                bottomSS.BoundarySymbol = cs;
                IRenderPolygon rBottom = d3.ObjectManager.CreateRenderPolygon(bottom, bottomSS, d3.ProjectTree.RootID);
                this._listRGuids.Add(rBottom.Guid);

                IGeometryFactory geoFact = new GeometryFactoryClass();
                IMultiPolygon side = geoFact.CreateGeometry(gviGeometryType.gviGeometryMultiPolygon, gviVertexAttribute.gviVertexAttributeZ) as IMultiPolygon;
                for (int i = 0; i < region.ExteriorRing.PointCount -1 ; i++)
                {
                    IPolygon gon = geoFact.CreateGeometry(gviGeometryType.gviGeometryPolygon, gviVertexAttribute.gviVertexAttributeZ) as IPolygon;
                    IPoint pt1 = region.ExteriorRing.GetPoint(i);
                    IPoint pt1temp = pt1.Clone() as IPoint;
                    if (pt1temp.Z < refheight) pt1temp.Z = refheight;
                    IPoint pt2 = region.ExteriorRing.GetPoint(i + 1);
                    IPoint pt2temp = pt2.Clone() as IPoint;
                    if (pt2temp.Z < refheight) pt2temp.Z = refheight;
                    IPoint pt3 = pt2.Clone() as IPoint;
                    pt3.Z = refheight;
                    IPoint pt4 = pt1.Clone() as IPoint;
                    pt4.Z = refheight;
                    gon.ExteriorRing.AppendPoint(pt1temp);
                    gon.ExteriorRing.AppendPoint(pt2temp);
                    gon.ExteriorRing.AppendPoint(pt3);
                    gon.ExteriorRing.AppendPoint(pt4);
                    gon.Close();
                    side.AddPolygon(gon);
                }
                ISurfaceSymbol sideSS = new SurfaceSymbol();
                sideSS.ImageName = System.Windows.Forms.Application.StartupPath + "\\..\\Resource\\Images\\TerrainHole\\TextureSide.jpg";
                sideSS.RepeatLengthU = 5;
                sideSS.RepeatLengthV = 5;
                sideSS.EnableLight = true;
                sideSS.BoundarySymbol = cs;
                IRenderMultiPolygon rSide = d3.ObjectManager.CreateRenderMultiPolygon(side, sideSS, d3.ProjectTree.RootID);
                this._listRGuids.Add(rSide.Guid);
            }
            catch (Exception ex)
            {

            }
        }
    }
}
