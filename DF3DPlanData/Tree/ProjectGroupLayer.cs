using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DFWinForms.LogicTree;
using DF3DPlanData.Class;
using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.RenderControl;

namespace DF3DPlanData.Tree
{
    //建设项目组
    public class ProjectGroupLayer
        : GroupLayerClass
    {

        private TerrainModifierForProject terrainModeifier;

        public ProjectGroupLayer()
        {
            base.ImageIndex = 13;
        }
        public ProjectGroupLayer(string id) : base(id) { }

        public PlanGroupLayer CreatePlan(ref UrbanPlan plan)
        {
            if ((plan == null) || (plan.Property == null))
            {
                return null;
            }
            PlanGroupLayer layer = new PlanGroupLayer(plan.Property.PlanID.ToString());
            layer.Name = plan.Property.PlanName;
            this.Add2(layer);
            layer.UPlan = plan;


            layer.Visible = plan.Visible;
            return layer;
        }

        private bool PraseBool(string ProjectRemark1)
        {
            bool result = false;
            if (bool.TryParse(ProjectRemark1, out result))
            {
                return result;
            }
            return result;
        }

        public CutFillResult ParseTerrainReuslt(string remarker)
        {
            try
            {
                if (string.IsNullOrEmpty(remarker) || !remarker.Contains(","))
                {
                    return null;
                }
                string[] strArray = remarker.Split(new char[] { ',' });
                if (strArray.Length != 4)
                {
                    return null;
                }
                return new CutFillResult { Flag = this.PraseBool(strArray[0]), Tolerance = Convert.ToDouble(strArray[1]), CutReuslt = Convert.ToDouble(strArray[2]), FillResult = Convert.ToDouble(strArray[3]) };
            }
            catch (Exception exception)
            {
                return null;
            }
        }
        private void CreateTerrainTerrainModifier(double elevation)
        {
            //IGeometry userRedLine = this.GetUserRedLine(); 修改
            IGeometry userRedLine = null;
            if (userRedLine != null)
            {
                IGeometry geometry2 = userRedLine.Clone2(gviVertexAttribute.gviVertexAttributeZ);
                if (geometry2 != null)
                {
                    IPolygon item = null;
                    List<IPolygon> regions = new List<IPolygon>();
                    if (geometry2.GeometryType == gviGeometryType.gviGeometryPolygon)
                    {
                        item = geometry2 as IPolygon;
                        regions.Add(item);
                    }
                    else if (geometry2.GeometryType == gviGeometryType.gviGeometryMultiPolygon)
                    {
                        IMultiPolygon polygon2 = geometry2 as IMultiPolygon;
                        if ((polygon2 == null) || (polygon2.GeometryCount <= 0))
                        {
                            return;
                        }
                        for (int i = 0; i < polygon2.GeometryCount; i++)
                        {
                            regions.Add(polygon2.GetPolygon(i));
                        }
                    }
                    if ((item != null) && (this.terrainModeifier != null))
                    {
                        this.terrainModeifier.CreateTerrainModifier(regions, elevation, gviElevationBehaviorMode.gviElevationBehaviorReplace);
                    }
                }
            }
        }

        public void SetTerrainModifier(string remarkString, double elevation)
        {
            CutFillResult result = this.ParseTerrainReuslt(remarkString);
            if ((result != null) && result.Flag)
            {
                if (this.terrainModeifier == null)
                {
                    this.terrainModeifier = new TerrainModifierForProject();
                }
                this.CreateTerrainTerrainModifier(elevation);
            }
        }



    }
}
