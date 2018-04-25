using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DF3DControl.Command;
using DF3DControl.UserControl.View;
using DFWinForms.Service;
using DF3DControl.Base;
using Gvitech.CityMaker.RenderControl;
using DFDataConfig.Class;
using DF3DData.Class;
using DevExpress.XtraEditors;
using Gvitech.CityMaker.FdeCore;
using DF3DPipeCreateTool.Class;
using DFAlgorithm.Network;
using DF3DPipeCreateTool.Service;
using DF3DDraw;
using DFCommon.Class;
using System.Windows.Forms;
using DFWinForms.Class;
using Gvitech.CityMaker.FdeGeometry;

namespace DF3DPipe.Analysis.Command
{
    class CmdCheckConnection: AbstractMap3DCommand
    {
        private DrawTool _drawTool;
        private List<Guid> _listRGuid = new List<Guid>();
        private int _startOid;
        private string _startFCGuid;
        private bool _bFinished;
        
        public override void Run(object sender, System.EventArgs e)
        {
            Map3DCommandManager.Push(this);
            this._bFinished = true;
            this._drawTool = DrawToolService.Instance.CreateDrawTool(DrawType.SelectOne);
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

        private void Clear()
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

        private void OnStartDraw()
        {
            if (this._drawTool != null)
            {
                //Clear();
            }
        }

        private void OnFinishedDraw()
        {
            if (this._drawTool != null && this._drawTool.GeoType == DrawType.SelectOne
                && this._drawTool.GetSelectFeatureLayerPickResult() != null && this._drawTool.GetSelectPoint() != null)
            {
                ClickQuery();
            }
        }

        private void ClickQuery()
        {
            DF3DApplication app = DF3DApplication.Application;
            if (app == null || app.Current3DMapControl == null) return;
            try
            {
                IFeatureLayer fl = this._drawTool.GetSelectFeatureLayerPickResult().FeatureLayer;
                if (fl == null) return;
                int featureId = this._drawTool.GetSelectFeatureLayerPickResult().FeatureId;
                FacilityClass facc = Dictionary3DTable.Instance.GetFacilityClassByDFFeatureClassID(fl.FeatureClassId.ToString());
                if (facc == null || facc.Name != "PipeNode")
                {
                    XtraMessageBox.Show("您选中的不是管点设施，请选择管点设施。", "提示");
                    return;
                }
                DF3DFeatureClass dffc = DF3DFeatureClassManager.Instance.GetFeatureClassByID(fl.FeatureClassId.ToString());
                if (dffc == null || dffc.GetFeatureClass() == null) return;
                IFeatureClass fc = dffc.GetFeatureClass();
                if (this._bFinished)
                {
                    Clear();
                    this._startFCGuid = fc.Guid.ToString();
                    this._startOid = featureId;
                    this._bFinished = false;
                    
                    ILabel label = app.Current3DMapControl.ObjectManager.CreateLabel(app.Current3DMapControl.ProjectTree.RootID);
                    label.Text = "起点";
                    ITextSymbol ts = new TextSymbol();
                    ts.TextAttribute.TextSize = SystemInfo.Instance.TextSize;
                    ts.TextAttribute.TextColor = Convert.ToUInt32(SystemInfo.Instance.TextColor, 16);
                    label.TextSymbol = ts;
                    label.Position = this._drawTool.GetSelectPoint();
                    this._listRGuid.Add(label.Guid);
                }
                else
                {
                    if (this._startFCGuid == fc.Guid.ToString() && this._startOid == featureId)
                    {
                        XtraMessageBox.Show("您选中的是同一个管点设施。", "提示");
                        return;
                    }
                    this._bFinished = true;
                    ILabel label = app.Current3DMapControl.ObjectManager.CreateLabel(app.Current3DMapControl.ProjectTree.RootID);
                    label.Text = "终点";
                    ITextSymbol ts = new TextSymbol();
                    ts.TextAttribute.TextSize = SystemInfo.Instance.TextSize;
                    ts.TextAttribute.TextColor = Convert.ToUInt32(SystemInfo.Instance.TextColor, 16);
                    label.TextSymbol = ts;
                    label.Position = this._drawTool.GetSelectPoint();
                    this._listRGuid.Add(label.Guid);
                    if (this._startFCGuid != fc.Guid.ToString())
                    {
                        XtraMessageBox.Show("您选中的不是同一类管点设施。", "提示");
                        return;
                    }
                    else
                    {
                        WaitForm.Start("正在分析...", "请稍后");
                        FacClassReg reg = FacilityInfoService.GetFacClassRegByFeatureClassID(fc.Guid.ToString());
                        if (reg == null)
                        {
                            return;
                        }
                        TopoClass tc = FacilityInfoService.GetTopoClassByFacClassCode(reg.FacClassCode);
                        if (tc == null)
                        {
                            return;
                        }
                        TopoNetwork net = tc.GetNetwork();
                        if (net == null)
                        {
                            XtraMessageBox.Show("构建拓扑网络失败！", "提示");
                            return;
                        }
                        else
                        {
                            string startId = this._startFCGuid + "_" + this._startOid.ToString();
                            string endId = fc.Guid.ToString() + "_" + featureId.ToString();
                            List<string> path;
                            double shortestLength = net.SPFA(startId, endId, out path);
                            if ((shortestLength > 0.0 && shortestLength != double.MaxValue) || (path != null && path.Count > 0))
                            {
                                List<IPoint> listPt = new List<IPoint>();
                                foreach (string nodeId in path)
                                {
                                    int index = nodeId.LastIndexOf("_");
                                    string fcguid = nodeId.Substring(0, index);
                                    string oid = nodeId.Substring(index + 1, nodeId.Length - index - 1);
                                    DF3DFeatureClass dffcTemp = DF3DFeatureClassManager.Instance.GetFeatureClassByID(fcguid);
                                    if (dffcTemp == null || dffcTemp.GetFeatureClass() == null) continue;
                                    if (dffcTemp.GetFacilityClassName() != "PipeNode") continue;
                                    IQueryFilter filter = new QueryFilter();
                                    filter.WhereClause = "oid = " + oid;
                                    filter.SubFields = "oid,Shape";
                                    IRowBuffer row = null;
                                    IFdeCursor cursor = null;
                                    try
                                    {
                                        cursor = dffcTemp.GetFeatureClass().Search(filter, false);
                                        while ((row = cursor.NextRow()) != null)
                                        {
                                            if (!row.IsNull(1) && (row.GetValue(1) is IGeometry))
                                            {
                                                IGeometry geo = row.GetValue(1) as IGeometry;
                                                switch (geo.GeometryType)
                                                {
                                                    case gviGeometryType.gviGeometryPoint:
                                                        IPoint pt = geo as IPoint;
                                                        pt.Z = pt.Z + 1;
                                                        listPt.Add(pt);
                                                        break;
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
                                IPolyline polyline = (new GeometryFactory()).CreateGeometry(gviGeometryType.gviGeometryPolyline, gviVertexAttribute.gviVertexAttributeZ) as IPolyline;
                                foreach (IPoint pt in listPt)
                                {
                                    ISimplePointSymbol ps = new SimplePointSymbol();
                                    ps.Size = 5;
                                    ps.Style = gviSimplePointStyle.gviSimplePointCircle;
                                    ps.FillColor = Convert.ToUInt32(SystemInfo.Instance.FillColor, 16);
                                    IRenderPoint rp = app.Current3DMapControl.ObjectManager.CreateRenderPoint(pt, ps, app.Current3DMapControl.ProjectTree.RootID);
                                    rp.Glow(5000);
                                    polyline.AppendPoint(pt);
                                    this._listRGuid.Add(rp.Guid);
                                }
                                ICurveSymbol cs = new CurveSymbol();
                                cs.Color = Convert.ToUInt32(SystemInfo.Instance.LineColor, 16);
                                cs.Width = -5;
                                IRenderPolyline rpl = app.Current3DMapControl.ObjectManager.CreateRenderPolyline(polyline, cs, app.Current3DMapControl.ProjectTree.RootID);
                                rpl.Glow(5000);
                                this._listRGuid.Add(rpl.Guid);
                            }
                            else
                            {
                                XtraMessageBox.Show("两点不连通！", "提示");
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("分析出错！", "提示");
            }
            finally
            {
                WaitForm.Stop();
            }
        }

    }
}
