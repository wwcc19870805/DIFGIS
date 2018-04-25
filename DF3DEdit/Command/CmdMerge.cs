using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using Gvitech.CityMaker.Common;
using Gvitech.CityMaker.FdeCore;
using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.Math;
using Gvitech.CityMaker.RenderControl;
using Gvitech.CityMaker.Resource;
using ICSharpCode.Core;
using DF3DControl.Command;
using DF3DControl.Base;
using DF3DData.Class;
using DF3DEdit.Service;
using DF3DEdit.Class;
using DF3DEdit.Frm;

namespace DF3DEdit.Command
{
    public class CmdMerge : AbstractMap3DCommand
    {
        public override void Run(object sender, System.EventArgs e)
        {
            DF3DApplication app = DF3DApplication.Application;
            if (app == null || app.Current3DMapControl == null) return;
            try
            {
                Map3DCommandManager.Push(this);
                RenderControlEditServices.Instance().StopGeometryEdit(true);
                app.Current3DMapControl.PauseRendering(false);
                System.Collections.Generic.IList<System.Collections.Generic.KeyValuePair<int, string>> list = new System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<int, string>>();
                HashMap featureClassInfoMap = SelectCollection.Instance().FeatureClassInfoMap;
                DF3DFeatureClass featureClassInfo = null;
                System.Collections.Generic.IList<int> list2 = new System.Collections.Generic.List<int>();
                System.Collections.IEnumerator enumerator = featureClassInfoMap.Keys.GetEnumerator();
                try
                {
                    if (enumerator.MoveNext())
                    {
                        DF3DFeatureClass featureClassInfo2 = (DF3DFeatureClass)enumerator.Current;
                        featureClassInfo = featureClassInfo2;
                        ResultSetInfo resultSetInfo = featureClassInfoMap[featureClassInfo2] as ResultSetInfo;
                        foreach (DataRow dataRow in resultSetInfo.ResultSetTable.Rows)
                        {
                            int num = int.Parse(dataRow[featureClassInfo.GetFeatureClass().FidFieldName].ToString());
                            string value = num.ToString();
                            System.Collections.Generic.KeyValuePair<int, string> item = new System.Collections.Generic.KeyValuePair<int, string>(num, value);
                            list.Add(item);
                            list2.Add(num);
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
                if (featureClassInfo != null)
                {
                    using (MergeDlg mergeDlg = new MergeDlg(list))
                    {
                        if (mergeDlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            if (System.Windows.Forms.DialogResult.No != XtraMessageBox.Show("模型合并不支持撤销操作，是否继续？", "提示", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Exclamation))
                            {
                                int fid = mergeDlg.Fid;
                                using (new WaitDialogForm("", "正在进行模型合并,请稍后..."))
                                {
                                    IFeatureClass featureClass = featureClassInfo.GetFeatureClass();
                                    string geometryFieldName = featureClassInfo.GetFeatureLayer().GeometryFieldName;
                                    IModelPoint model = this.GetModel(featureClass, geometryFieldName, fid);
                                    IResourceManager resourceManager = CommonUtils.Instance().GetCurrentFeatureDataset() as IResourceManager;
                                    if (resourceManager != null)
                                    {
                                        if (!this.MergeModels(featureClass, geometryFieldName, list2.ToArray<int>(), resourceManager, ref model))
                                        {
                                            XtraMessageBox.Show("模型合并失败!");
                                        }
                                        else
                                        {
                                            if (list2.Remove(fid))
                                            {
                                                featureClass.Delete(new QueryFilterClass
                                                {
                                                    IdsFilter = list2.ToArray<int>()
                                                });
                                                CommonUtils.Instance().Delete(featureClassInfo, list2.ToArray<int>());
                                                app.Current3DMapControl.FeatureManager.DeleteFeatures(featureClass, list2.ToArray<int>());
                                            }
                                            app.Current3DMapControl.RefreshModel(CommonUtils.Instance().GetCurrentFeatureDataset(), model.ModelName);
                                            IFdeCursor fdeCursor = featureClass.Update(new QueryFilterClass
                                            {
                                                IdsFilter = new int[]
												{
													fid
												}
                                            });
                                            IRowBuffer rowBuffer = fdeCursor.NextRow();
                                            if (rowBuffer != null)
                                            {
                                                int num2 = rowBuffer.FieldIndex(geometryFieldName);
                                                if (num2 != -1)
                                                {
                                                    rowBuffer.SetValue(num2, model);
                                                }
                                                fdeCursor.UpdateRow(rowBuffer);
                                                System.Runtime.InteropServices.Marshal.ReleaseComObject(fdeCursor);
                                                app.Current3DMapControl.FeatureManager.EditFeature(featureClass, rowBuffer);
                                            }
                                            //System.Runtime.InteropServices.Marshal.ReleaseComObject(featureClass);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (System.Runtime.InteropServices.COMException ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
            catch (System.UnauthorizedAccessException var_23_389)
            {
                XtraMessageBox.Show("拒绝访问");
            }
            catch (System.Exception ex2)
            {
                XtraMessageBox.Show(ex2.Message);
            }
            finally
            {
                app.Current3DMapControl.ResumeRendering();
            }
        }

        public override void RestoreEnv()
        {
        }

        private IModelPoint GetModel(IFeatureClass fc, string geoFieldName, int oid)
        {
            IModelPoint result = null;
            IRowBuffer row = fc.GetRow(oid);
            int num = row.FieldIndex(geoFieldName);
            if (num != -1)
            {
                result = (row.GetValue(num) as IModelPoint);
            }
            return result;
        }

        private bool MergeModels(IFeatureClass fc, string geometryField, int[] oidList, IResourceManager resMgr, ref IModelPoint desModelPoint)
        {
            bool flag = true;
            bool result;
            try
            {
                if (fc == null || oidList == null)
                {
                    result = false;
                    return result;
                }
                Gvitech.CityMaker.Resource.IModel model = resMgr.GetModel(desModelPoint.ModelName);
                IMatrix matrix = desModelPoint.AsMatrix().Clone();
                matrix.Inverse();
                int position = fc.GetFields().IndexOf(geometryField);
                IFdeCursor rows = fc.GetRows(oidList, false);
                IVector3 src = new Vector3Class();
                IVector3 vector = new Vector3Class();
                System.Collections.Generic.Dictionary<IMatrix, string> dictionary = new System.Collections.Generic.Dictionary<IMatrix, string>();
                IRowBuffer rowBuffer;
                while ((rowBuffer = rows.NextRow()) != null)
                {
                    IModelPoint modelPoint = rowBuffer.GetValue(position) as IModelPoint;
                    if (modelPoint != null)
                    {
                        dictionary[modelPoint.AsMatrix().Clone()] = modelPoint.ModelName;
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(modelPoint);
                    }
                }
                foreach (IMatrix current in dictionary.Keys)
                {
                    string name = dictionary[current];
                    Gvitech.CityMaker.Resource.IModel model2 = resMgr.GetModel(name);
                    for (int i = 0; i < model2.GroupCount; i++)
                    {
                        Gvitech.CityMaker.Resource.IDrawGroup drawGroup = new DrawGroupClass();
                        Gvitech.CityMaker.Resource.IDrawGroup group = model2.GetGroup(i);
                        for (int j = 0; j < group.PrimitiveCount; j++)
                        {
                            Gvitech.CityMaker.Resource.IDrawPrimitive primitive = group.GetPrimitive(j);
                            if (primitive.PrimitiveType == Gvitech.CityMaker.Resource.gviPrimitiveType.gviPrimitiveBillboardZ)
                            {
                                flag = false;
                                result = flag;
                                return result;
                            }
                            Gvitech.CityMaker.Resource.IDrawPrimitive drawPrimitive = new DrawPrimitiveClass();
                            IFloatArray vertexArray = primitive.VertexArray;
                            IFloatArray floatArray = new FloatArrayClass();
                            int num = 0;
                            while ((long)num < (long)((ulong)vertexArray.Length))
                            {
                                vector.X = (double)vertexArray.Get(num);
                                vector.Y = (double)vertexArray.Get(num + 1);
                                vector.Z = (double)vertexArray.Get(num + 2);
                                current.MultiplyVector(vector, ref src);
                                matrix.MultiplyVector(src, ref vector);
                                floatArray.Append((float)vector.X);
                                floatArray.Append((float)vector.Y);
                                floatArray.Append((float)vector.Z);
                                num += 3;
                            }
                            drawPrimitive.VertexArray = floatArray;
                            drawPrimitive.BakedTexcoordArray = primitive.BakedTexcoordArray;
                            drawPrimitive.ColorArray = primitive.ColorArray;
                            drawPrimitive.IndexArray = primitive.IndexArray;
                            drawPrimitive.Material = primitive.Material;
                            drawPrimitive.NormalArray = primitive.NormalArray;
                            drawPrimitive.PrimitiveMode = primitive.PrimitiveMode;
                            drawPrimitive.PrimitiveType = primitive.PrimitiveType;
                            drawPrimitive.TexcoordArray = primitive.TexcoordArray;
                            drawGroup.AddPrimitive(drawPrimitive);
                        }
                        drawGroup.CompleteMapFactor = group.CompleteMapFactor;
                        drawGroup.CompleteMapTextureName = group.CompleteMapTextureName;
                        drawGroup.LightMapTextureName = group.LightMapTextureName;
                        model.AddGroup(drawGroup);
                    }
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(model2);
                }
                System.Runtime.InteropServices.Marshal.ReleaseComObject(rows);
                desModelPoint.ModelEnvelope = model.Envelope.Clone();
                resMgr.UpdateModel(desModelPoint.ModelName, model);
                resMgr.RebuildSimplifiedModel(desModelPoint.ModelName);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(matrix);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(model);
            }
            catch (System.Runtime.InteropServices.COMException ex)
            {
                flag = false;
                XtraMessageBox.Show(ex.Message);
            }
            catch (System.Exception e)
            {
                flag = false;
                LoggingService.Error(e);
            }
            result = flag;
            return result;
        }
    }
}
