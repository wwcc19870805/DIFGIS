using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.RenderControl;

namespace DF3DDraw
{
    public class Draw3DModel : DrawTool
    {
        private string _filePath;
        private IRenderModelPoint _rmp;
        private bool bOper;
        public Draw3DModel()
        {
            this._geoType = DrawType._3DModel;
        }

        public override void Close()
        {
            if (this._rmp != null)
            {
                this._3DControl.ObjectManager.DeleteObject(this._rmp.Guid);
                this._rmp = null;
            }
        }

        public void Set3DModelFilePath(string filePath)
        {
            this._filePath = filePath;
            if (System.IO.File.Exists(this._filePath))
            {
                IModelPoint mp = this._geoFactory.CreateGeometry(gviGeometryType.gviGeometryModelPoint, Gvitech.CityMaker.FdeGeometry.gviVertexAttribute.gviVertexAttributeZ) as IModelPoint;
                mp.SetCoords(0, 0, 0, 0, 0);
                mp.ModelName = this._filePath;
                this._rmp = this._3DControl.ObjectManager.CreateRenderModelPoint(mp, null, this._rootID);
                this._rmp.VisibleMask = gviViewportMask.gviViewAllNormalView;
                this._3DControl.ObjectEditor.StartEditRenderGeometry(this._rmp, gviGeoEditType.gviGeoEditCreator);
            }
        }

        public override void Start()
        {
            if (System.IO.File.Exists(this._filePath) && !bOper)
            {
                this._3DControl.InteractMode = Gvitech.CityMaker.RenderControl.gviInteractMode.gviInteractEdit;
                this._3DControl.RcObjectEditing += new Gvitech.CityMaker.Controls._IRenderControlEvents_RcObjectEditingEventHandler(_3DControl_RcObjectEditing);
                this._3DControl.RcObjectEditFinish += new EventHandler(_3DControl_RcObjectEditFinish);
                this._3DControl.ObjectEditor.StartEditRenderGeometry(this._rmp, gviGeoEditType.gviGeoEditCreator);
                bOper = true;
            }
        }

        void _3DControl_RcObjectEditFinish(object sender, EventArgs e)
        {
            if (this._onFinishedDraw != null) this._onFinishedDraw();
        }

        void _3DControl_RcObjectEditing(object sender, Gvitech.CityMaker.Controls._IRenderControlEvents_RcObjectEditingEvent e)
        {
            this._geo = e.geometry;
        }

        public override void End()
        {
            if (System.IO.File.Exists(this._filePath) && bOper)
            {
                this._3DControl.InteractMode = gviInteractMode.gviInteractNormal;
                this._3DControl.RcObjectEditing -= new Gvitech.CityMaker.Controls._IRenderControlEvents_RcObjectEditingEventHandler(_3DControl_RcObjectEditing);
                this._3DControl.RcObjectEditFinish -= new EventHandler(_3DControl_RcObjectEditFinish);
                bOper = false;
            }
        }

    }
}
