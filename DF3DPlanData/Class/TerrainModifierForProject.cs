using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gvitech.CityMaker.RenderControl;
using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.Controls;

namespace DF3DPlanData.Class
{
    public class TerrainModifierForProject
    {
        // Fields
        private List<ITerrainModifier> _terrainModifierList;
        private gviViewportMask gviviewportMask;
        private double terrainHeight;
        private AxRenderControl _3DControl = DF3DControl.Base.DF3DApplication.Application.Current3DMapControl;
        // Methods
        public bool ChechExistTerrainModifier()
        {
            if (this._terrainModifierList == null)
            {
                return false;
            }
            return true;
        }

        public ITerrainModifier CreateSingleTerrainModifier(IPolygon region, double height, gviElevationBehaviorMode elevationMode = 0)
        {
            try
            {
                IPolygon newVal = null;
                ITerrainModifier modifier = null;
                modifier = _3DControl.ObjectManager.CreateTerrainModifier(region, _3DControl.ProjectTree.RootID);
                if (modifier == null)
                {
                    return null;
                }
                IPoint pointValue = null;
                newVal = region.Clone() as IPolygon;
                for (int i = 0; i < region.ExteriorRing.PointCount; i++)
                {
                    pointValue = region.ExteriorRing.GetPoint(i);
                    pointValue.Z = height;
                    newVal.ExteriorRing.UpdatePoint(i, pointValue);
                }
                modifier.SetPolygon(newVal);
                modifier.ElevationBehavior = gviElevationBehaviorMode.gviElevationBehaviorReplace;
                return modifier;
            }
            catch (Exception exception)
            {
                return null;
            }
        }

        public bool CreateTerrainModifier(List<IPolygon> regions, double height, gviElevationBehaviorMode elevationMode = 0)
        {
            try
            {
                if ((regions == null) || (regions.Count <= 0))
                {
                    return false;
                }
                if (this._terrainModifierList == null)
                {
                    this._terrainModifierList = new List<ITerrainModifier>();
                }
                this.terrainHeight = height;
                foreach (IPolygon polygon in regions)
                {
                    ITerrainModifier item = this.CreateSingleTerrainModifier(polygon, height, elevationMode);
                    if (item != null)
                    {
                        this._terrainModifierList.Add(item);
                    }
                }
                this.gviviewportMask = gviViewportMask.gviViewAllNormalView;
                return true;
            }
            catch (Exception exception)
            {
                return false;
            }
        }

        private void DeleteObject(Guid guid)
        {
            if (_3DControl != null)
            {
                _3DControl.ObjectManager.DeleteObject(guid);
            }
        }

        public void DeleteTerrainModifier()
        {
            if ((this._terrainModifierList != null) && (this._terrainModifierList.Count > 0))
            {
                for (int i = 0; i < this._terrainModifierList.Count; i++)
                {
                    ITerrainModifier modifier = this._terrainModifierList[i];
                    this.DeleteObject(modifier.Guid);
                    modifier = null;
                }
                this._terrainModifierList.Clear();
                this._terrainModifierList = null;
            }
        }

        public void HideTerrainModifier()
        {
            this.SetTerrainModifier(gviViewportMask.gviViewNone);
        }

        private IPolygon ResetPolygonZ(IPolygon poly, double height)
        {
            if (poly == null)
            {
                return null;
            }
            int pointCount = poly.ExteriorRing.PointCount;
            IPoint pointValue = null;
            for (int i = 0; i < pointCount; i++)
            {
                pointValue = poly.ExteriorRing.GetPoint(i);
                pointValue.Z = height;
                poly.ExteriorRing.UpdatePoint(i, pointValue);
            }
            return poly;
        }

        public void ResetTerrainHole(double height)
        {
            if ((this._terrainModifierList != null) && (this._terrainModifierList.Count > 0))
            {
                foreach (ITerrainModifier modifier in this._terrainModifierList)
                {
                    modifier.VisibleMask = gviViewportMask.gviViewAllNormalView;
                    IPolygon newVal = this.ResetPolygonZ(modifier.GetPolygon(), height);
                    if (newVal != null)
                    {
                        modifier.SetPolygon(newVal);
                        modifier.VisibleMask = gviViewportMask.gviViewAllNormalView;
                        this.gviviewportMask = gviViewportMask.gviViewAllNormalView;
                    }
                }
            }
        }

        private void SetTerrainModifier(gviViewportMask mask)
        {
            if ((this._terrainModifierList != null) && (this._terrainModifierList.Count > 0))
            {
                this.gviviewportMask = mask;
                foreach (ITerrainModifier modifier in this._terrainModifierList)
                {
                    modifier.VisibleMask = mask;
                }
            }
        }

        public void ShowTerrainModifier()
        {
            this.SetTerrainModifier(gviViewportMask.gviViewAllNormalView);
        }

        // Properties
        public double TerrainHeight
        {
            get
            {
                return this.terrainHeight;
            }
            private set
            {
                this.terrainHeight = value;
            }
        }

        public gviViewportMask ViewportMask
        {
            get
            {
                return this.gviviewportMask;
            }
            private set
            {
                this.gviviewportMask = value;
            }
        }
    }
}
