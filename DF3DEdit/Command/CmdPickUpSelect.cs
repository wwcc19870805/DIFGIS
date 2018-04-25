using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DFWinForms.Service;
using DF3DControl.Command;
using DF3DControl.Base;
using DF3DControl.UserControl.View;
using DF3DEdit.Class;
using DF3DEdit.Service;
using DF3DData.Class;
using Gvitech.CityMaker.RenderControl;
using Gvitech.CityMaker.FdeCore;

namespace DF3DEdit.Command
{
    public class CmdPickUpSelect : AbstractMap3DCommand
    {
        public override void Run(object sender, System.EventArgs e)
        {
            Map3DCommandManager.Push(this);
            IMap3DView map3DView = UCService.GetContent(typeof(Map3DView)) as Map3DView;
            if (map3DView == null) return;
            bool b3DBind = map3DView.Bind(this);
            if (!b3DBind) return;
            DF3DApplication app = DF3DApplication.Application;
            if (app == null || app.Current3DMapControl == null) return;
            app.Current3DMapControl.InteractMode = gviInteractMode.gviInteractSelect;
            app.Current3DMapControl.MouseSelectObjectMask = gviMouseSelectObjectMask.gviSelectFeatureLayer;
            app.Current3DMapControl.MouseSelectMode = gviMouseSelectMode.gviMouseSelectClick;
        }

        public override void RestoreEnv()
        {
            //RenderControlEditServices.Instance().StopGeometryEdit(true);
            //FDECommand fDECommand = new FDECommand(true, false);
            //SelectCollection.Instance().Clear();
            //SelectCollection.Instance().ClearRowBuffers();
            //RenderControlEditServices.Instance().SetEditorPosition(null);
            //fDECommand.SetSelectionMap();

            IMap3DView map3DView = UCService.GetContent(typeof(Map3DView)) as Map3DView;
            if (map3DView == null) return;
            map3DView.UnBind(this);
            DF3DApplication app = DF3DApplication.Application;
            if (app == null || app.Current3DMapControl == null) return;
            app.Current3DMapControl.InteractMode = gviInteractMode.gviInteractNormal;
            app.Current3DMapControl.MouseSelectObjectMask = gviMouseSelectObjectMask.gviSelectNone;
            app.Current3DMapControl.MouseSelectMode = gviMouseSelectMode.gviMouseSelectHover;
        }

        public override void RcMouseClickSelect(object sender, Gvitech.CityMaker.Controls._IRenderControlEvents_RcMouseClickSelectEvent e)
        {
            if (e.eventSender == gviMouseSelectMode.gviMouseSelectClick)
            {
                this.MousePickup(e.pickResult, e.mask);
            }
        }

        private void MousePickup(IPickResult pr, gviModKeyMask mask)
        {
            DF3DApplication app = DF3DApplication.Application;
            if (app == null || app.Current3DMapControl == null) return;
            if ((mask == gviModKeyMask.gviModKeyCtrl || mask == gviModKeyMask.gviModKeyShift) && pr == null)
            {
                return;
            }
            //FDECommand fDECommand = new FDECommand(true, false);
            if (mask != gviModKeyMask.gviModKeyCtrl && mask != gviModKeyMask.gviModKeyShift)
            {
                app.Current3DMapControl.FeatureManager.UnhighlightAll();
            }
            else
            {
                HashMap featureClassInfoMap = SelectCollection.Instance().FeatureClassInfoMap;
                if (featureClassInfoMap != null)
                {
                    bool flag = true;
                    System.Collections.IEnumerator enumerator = featureClassInfoMap.Keys.GetEnumerator();
                    try
                    {
                        if (enumerator.MoveNext())
                        {
                            DF3DFeatureClass featureClassInfo = (DF3DFeatureClass)enumerator.Current;
                            if (featureClassInfo.GetFeatureClass().Guid == CommonUtils.Instance().CurEditLayer.GetFeatureClass().Guid)
                            {
                                flag = true;
                            }
                        }
                    }
                    finally
                    {
                        System.IDisposable disposable = enumerator as System.IDisposable;
                        if (disposable != null)
                        {
                            disposable.Dispose();
                        }
                    }
                    if (!flag)
                    {
                        SelectCollection.Instance().Clear();
                    }
                }
            }
            if (app.Current3DMapControl.ObjectEditor.IsEditing)
            {
                RenderControlEditServices.Instance().StopGeometryEdit(true);
            }
            if (pr == null)
            {
                SelectCollection.Instance().Clear();
                app.Current3DMapControl.TransformHelper.Type = gviEditorType.gviEditorNone;
            }
            else
            {
                if (pr.Type == gviObjectType.gviObjectTerrain || pr.Type == gviObjectType.gviObjectReferencePlane)
                {
                    SelectCollection.Instance().Clear();
                    app.Current3DMapControl.TransformHelper.Type = gviEditorType.gviEditorNone;
                }
                else
                {
                    if (pr.Type == gviObjectType.gviObjectFeatureLayer)
                    {
                        IFeatureLayerPickResult featureLayerPickResult = pr as IFeatureLayerPickResult;
                        int featureId = featureLayerPickResult.FeatureId;
                        IFeatureLayer featureLayer = featureLayerPickResult.FeatureLayer;
                        if (featureLayer.Guid == CommonUtils.Instance().CurEditLayer.GetFeatureLayer().Guid)
                        {
                            DF3DFeatureClass featureClassInfo = DF3DFeatureClassManager.Instance.GetFeatureClassByID(featureLayer.FeatureClassId.ToString());
                            if (featureClassInfo != null)
                            {
                                IFeatureClass featureClass = featureClassInfo.GetFeatureClass();
                                HashMap hashMap = new HashMap();
                                if (mask == (gviModKeyMask)0)
                                {
                                    hashMap[featureClassInfo] = new System.Collections.Generic.List<int>
								    {
									    featureId
								    };
                                }
                                else
                                {
                                    foreach (DF3DFeatureClass key in SelectCollection.Instance().FeatureClassInfoMap.Keys)
                                    {
                                        ResultSetInfo resultSetInfo = SelectCollection.Instance().FeatureClassInfoMap[key] as ResultSetInfo;
                                        if (resultSetInfo != null && resultSetInfo.OidList != null)
                                        {
                                            hashMap[key] = resultSetInfo.OidList;
                                        }
                                    }
                                    System.Collections.Generic.List<int> list = hashMap[featureClassInfo] as System.Collections.Generic.List<int>;
                                    if (list != null)
                                    {
                                        if (list.Contains(featureId))
                                        {
                                            app.Current3DMapControl.FeatureManager.UnhighlightFeature(featureClass, featureId);
                                            list.Remove(featureId);
                                        }
                                        else
                                        {
                                            list.Add(featureId);
                                        }
                                    }
                                    else
                                    {
                                        hashMap[featureClassInfo] = new System.Collections.Generic.List<int>
									{
										featureId
									};
                                    }
                                }
                                SelectCollection.Instance().UpdateSelection(hashMap);
                            }
                            else
                            {
                                SelectCollection.Instance().Clear();
                                app.Current3DMapControl.TransformHelper.Type = gviEditorType.gviEditorNone;
                            }
                        }
                    }
                }
            }
            RenderControlEditServices.Instance().SetEditorPosition(SelectCollection.Instance().FcRowBuffersMap);
            //fDECommand.SetSelectionMap();
            //CommandManagerServices.Instance().CallCommand(fDECommand);
            app.Workbench.UpdateMenu();
        }
    }
}
