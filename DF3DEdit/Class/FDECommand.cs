using System;
using System.Collections.Generic;
using Gvitech.CityMaker.Math;
using DF3DControl.Base;
using DF3DData.Class;
using DF3DEdit.Service;

namespace DF3DEdit.Class
{
    public class FDECommand : BaseCommand
    {
        private bool _bSelectionChanged;
        private bool _bFeatureModify;

        public FDECommand(bool bSelectionChanged, bool bFeatureModify)
        {
            this._bSelectionChanged = bSelectionChanged;
            this._bFeatureModify = bFeatureModify;
            IVector3 vector;
            IEulerAngle eulerAngle;
            DF3DApplication.Application.Current3DMapControl.Camera.GetCamera(out vector, out eulerAngle);
            CameraParamter undoCamera = new CameraParamter(vector.X, vector.Y, vector.Z, eulerAngle.Heading, eulerAngle.Tilt, eulerAngle.Roll);
            base.UndoCamera = undoCamera;
            if (this._bSelectionChanged)
            {
                HashMap selectionMap = this.GetSelectionMap();
                base.UndoSelectionMap = selectionMap;
            }
        }
        public override bool Undo()
        {
            if (this._bSelectionChanged)
            {
                SelectCollection.Instance().Clear();
                SelectCollection.Instance().UpdateSelection(base.UndoSelectionMap);
                RenderControlEditServices.Instance().SetEditorPosition(SelectCollection.Instance().FcRowBuffersMap);
            }
            if (this._bFeatureModify)
            {
                CommonUtils.Instance().FdeUndoRedoManager.Undo();
            }
            IVector3 vector;
            IEulerAngle eulerAngle;
            DF3DApplication.Application.Current3DMapControl.Camera.GetCamera(out vector, out eulerAngle);
            CameraParamter redoCamera = new CameraParamter(vector.X, vector.Y, vector.Z, eulerAngle.Heading, eulerAngle.Tilt, eulerAngle.Roll);
            base.RedoCamera = redoCamera;
            this.SetUndoCamera();
            return true;
        }
        public override bool Redo()
        {
            if (this._bSelectionChanged)
            {
                SelectCollection.Instance().Clear();
                SelectCollection.Instance().UpdateSelection(base.RedoSelectionMap);
                RenderControlEditServices.Instance().SetEditorPosition(SelectCollection.Instance().FcRowBuffersMap);
            }
            if (this._bFeatureModify)
            {
                CommonUtils.Instance().FdeUndoRedoManager.Redo();
            }
            this.SetRedoCamera();
            return true;
        }
        private void SetUndoCamera()
        {
            if (base.UndoCamera != null && base.UndoCamera.IsCameraChanged)
            {
                RenderControlServices.Instance().SetCamera(base.UndoCamera.X, base.UndoCamera.Y, base.UndoCamera.Z, base.UndoCamera.Heading, base.UndoCamera.Tilt, base.UndoCamera.Roll);
            }
        }
        private void SetRedoCamera()
        {
            if (base.RedoCamera != null && base.RedoCamera.IsCameraChanged)
            {
                RenderControlServices.Instance().SetCamera(base.RedoCamera.X, base.RedoCamera.Y, base.RedoCamera.Z, base.RedoCamera.Heading, base.RedoCamera.Tilt, base.RedoCamera.Roll);
            }
        }
        private HashMap GetSelectionMap()
        {
            if (SelectCollection.Instance().FeatureClassInfoMap == null || SelectCollection.Instance().FeatureClassInfoMap.Count == 0)
            {
                return null;
            }
            HashMap hashMap = new HashMap();
            foreach (DF3DFeatureClass key in SelectCollection.Instance().FeatureClassInfoMap.Keys)
            {
                ResultSetInfo resultSetInfo = SelectCollection.Instance().FeatureClassInfoMap[key] as ResultSetInfo;
                if (resultSetInfo != null && resultSetInfo.ResultSetTable != null && resultSetInfo.ResultSetTable.Rows.Count != 0)
                {
                    System.Collections.Generic.List<int> list = new System.Collections.Generic.List<int>();
                    for (int i = 0; i < resultSetInfo.ResultSetTable.Rows.Count; i++)
                    {
                        int item = -1;
                        if (int.TryParse(resultSetInfo.ResultSetTable.Rows[i][0].ToString(), out item))
                        {
                            list.Add(item);
                        }
                    }
                    hashMap[key] = list;
                }
            }
            return hashMap;
        }
        public void SetSelectionMap()
        {
            base.RedoSelectionMap = this.GetSelectionMap();
        }
    }
}
