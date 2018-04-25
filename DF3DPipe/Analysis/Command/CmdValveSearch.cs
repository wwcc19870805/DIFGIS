using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DF3DDraw;
using DF3DControl.Command;
using DF3DControl.Base;
using Gvitech.CityMaker.RenderControl;
using DFDataConfig.Class;
using DevExpress.XtraEditors;
using DF3DData.Class;
using Gvitech.CityMaker.FdeCore;
using DFCommon.Class;
using System.IO;
using System.Windows.Forms;
using Gvitech.CityMaker.FdeGeometry;
using DFDataConfig.Logic;
using Gvitech.CityMaker.Math;
using DF3DPipeCreateTool.Service;
using DF3DPipeCreateTool.Class;
using DFAlgorithm.Network;
using Newtonsoft.Json.Linq;
using DFWinForms.Class;

namespace DF3DPipe.Analysis.Command
{
    class CmdValveSearch: AbstractMap3DCommand
    {
        private DrawTool _drawTool;
        private List<Guid> _listRGuid = new List<Guid>();
        private IRenderPoint _rPoint;
        private IParticleEffect _particleEffect;
        public override void Run(object sender, System.EventArgs e)
        {
            Map3DCommandManager.Push(this);
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
            if (_rPoint != null)
            {
                app.Current3DMapControl.ObjectManager.DeleteObject(_rPoint.Guid);
                _rPoint = null;
            }
            if (_particleEffect != null)
            {
                app.Current3DMapControl.ObjectManager.DeleteObject(_particleEffect.Guid);
                _particleEffect = null;
            }
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
                if (facc == null || facc.Name != "PipeLine")
                {
                    XtraMessageBox.Show("您选中的不是管线设施，请选择管线设施。", "提示");
                    return;
                }
                DF3DFeatureClass dffc = DF3DFeatureClassManager.Instance.GetFeatureClassByID(fl.FeatureClassId.ToString());
                if (dffc == null || dffc.GetFeatureClass() == null) return;
                IFeatureClass fc = dffc.GetFeatureClass();
                IImagePointSymbol ips = new ImagePointSymbol();
                ips.Size = SystemInfo.Instance.SymbolSize;
                ips.ImageName = Path.Combine(Application.StartupPath, "..\\Resource\\Images\\POI\\accidentPoint.png");

                IPoint intersectPoint = this._drawTool.GetSelectPoint();

                _rPoint = app.Current3DMapControl.ObjectManager.CreateRenderPoint(intersectPoint, ips, app.Current3DMapControl.ProjectTree.RootID);
                _rPoint.MinVisibleDistance = 499;
                _rPoint.MaxVisibleDistance = 99999;

                _particleEffect = app.Current3DMapControl.ObjectManager.CreateParticleEffect(app.Current3DMapControl.ProjectTree.RootID);
                MajorClass mc = LogicDataStructureManage3D.Instance.GetMajorClassByDFFeatureClassID(fl.FeatureClassId.ToString());
                if (mc != null && (mc.Name == "PS" || mc.Name == "GS"))
                {
                    _particleEffect.ImageName = Path.Combine(Application.StartupPath, "..\\Resource\\Images\\ParticleEffect\\water.png");

                    _particleEffect.ParticleBillboardType = gviParticleBillboardType.gviParticleBillboardOrientedMoveDirection;
                    //_particleEffect.setTextureTileRange(8, 8, 63, 63); 
                    _particleEffect.EmissionMinRate = 1600;
                    _particleEffect.EmissionMaxRate = 1300;
                    _particleEffect.EmissionMinAngle = 0;
                    _particleEffect.EmissionMaxAngle = 0.025 * Math.PI;   // 改成一个圆
                    _particleEffect.EmissionMinMoveSpeed = 25;
                    _particleEffect.EmissionMaxMoveSpeed = 30;
                    _particleEffect.EmissionMinRotationSpeed = 0;
                    _particleEffect.EmissionMaxRotationSpeed = 0;

                    _particleEffect.ParticleMinLifeTime = 4.5;
                    _particleEffect.ParticleMaxLifeTime = 5.5;
                    _particleEffect.EmissionMinParticleSize = 0.2;
                    _particleEffect.EmissionMaxParticleSize = 0.25;

                    _particleEffect.EmissionMinScaleSpeed = 0;
                    _particleEffect.EmissionMaxScaleSpeed = 0;
                    _particleEffect.ParticleBirthColor = 0xffffffff;
                    _particleEffect.ParticleDeathColor = 0x00ffffff;
                    _particleEffect.VerticalAcceleration = 5;
                    _particleEffect.Damping = 0.5;
                    _particleEffect.WindAcceleration = 0;
                    _particleEffect.WindDirection = 0;
                    IEulerAngle v3t = new EulerAngle();
                    v3t.Set(90, 45, 0);
                    _particleEffect.EmissionDirectionEulerAngle = v3t;
                }
                else
                {
                    _particleEffect.ImageName = Path.Combine(Application.StartupPath, "..\\Resource\\Images\\ParticleEffect\\smoke1.png");
                    _particleEffect.ParticleBillboardType = gviParticleBillboardType.gviParticleBillboardOrientedCamera;
                    _particleEffect.SetTextureTileRange(8, 8, 63, 63);  // 58,58位置的图有问题
                    _particleEffect.EmissionMinRate = 20;
                    _particleEffect.EmissionMaxRate = 30;
                    _particleEffect.EmissionMinAngle = 0;
                    _particleEffect.EmissionMaxAngle = 3.14 * 2.0;   // 改成一个圆
                    _particleEffect.EmissionMinMoveSpeed = 0;
                    _particleEffect.EmissionMaxMoveSpeed = 1;
                    _particleEffect.EmissionMinRotationSpeed = -1;
                    _particleEffect.EmissionMaxRotationSpeed = 1;

                    _particleEffect.ParticleMinLifeTime = 10;
                    _particleEffect.ParticleMaxLifeTime = 12;
                    _particleEffect.EmissionMinParticleSize = 0.75;
                    _particleEffect.EmissionMaxParticleSize = 0.9;

                    _particleEffect.EmissionMinScaleSpeed = 1.5;
                    _particleEffect.EmissionMaxScaleSpeed = 1.85;
                    _particleEffect.ParticleBirthColor = 0xffffffff;
                    _particleEffect.ParticleDeathColor = 0x00ffffff;
                    _particleEffect.VerticalAcceleration = -2;
                    _particleEffect.Damping = 0;
                    _particleEffect.WindAcceleration = 0;
                    _particleEffect.WindDirection = 0;
                }
                _particleEffect.MinVisibleDistance = 0;
                _particleEffect.MaxVisibleDistance = 500;

                _particleEffect.Start(-1);
                IPoint pttemp = (new GeometryFactoryClass()).CreateGeometry(gviGeometryType.gviGeometryPoint, gviVertexAttribute.gviVertexAttributeZ) as IPoint;
                IVector3 v3 = new Vector3();
                v3.Set(intersectPoint.X, intersectPoint.Y, intersectPoint.Z);
                pttemp.Position = v3;
                _particleEffect.SetCircleEmitter(pttemp, 0);

                this._drawTool.End();

                WaitForm.Start("正在分析...", "请稍后");
                string res = GetValveClosed(fl.FeatureClassId.ToString(), featureId.ToString());
                JObject json = JObject.Parse(res);
                if (json["res"] != null)
                {
                    string resstr = json["res"].ToString();
                    if (resstr == "false")
                    {
                        XtraMessageBox.Show("分析出错", "提示");
                        return;
                    }
                    else
                    {
                        string msgstr = json["msg"].ToString();
                        if (json["count"] == null)
                        {
                            XtraMessageBox.Show(msgstr, "提示");
                            return;
                        }
                        int count = int.Parse(json["count"].ToString());

                        double totalx = 0.0;
                        double totaly = 0.0;
                        double totalz = 0.0;

                        #region 渲染搜索到的阀门
                        if (json["preValveIds"] != null)
                        {
                            string preValveIds = json["preValveIds"].ToString();
                            string[] arr = preValveIds.Split(';');
                            foreach (string item in arr)
                            {
                                int index = item.LastIndexOf("_");
                                string fcguid = item.Substring(0, index);
                                string oid = item.Substring(index + 1, item.Length - index -1);
                                DF3DFeatureClass dffc1 = DF3DFeatureClassManager.Instance.GetFeatureClassByID(fcguid);
                                if (dffc != null)
                                {
                                    IFeatureClass fc1 = dffc1.GetFeatureClass();
                                    if (fc1 != null)
                                    {
                                        IFdeCursor cursor = null;
                                        IRowBuffer row = null;
                                        try
                                        {
                                            IQueryFilter filter = new QueryFilter();
                                            filter.WhereClause = "oid=" + oid;
                                            filter.SubFields = "oid,Shape";
                                            cursor = fc1.Search(filter, false);
                                            row = cursor.NextRow();
                                            if (row != null && !row.IsNull(1))
                                            {
                                                object obj = row.GetValue(1);
                                                if (obj is IGeometry)
                                                {
                                                    IGeometry geo = obj as IGeometry;
                                                    switch (geo.GeometryType)
                                                    {
                                                        case gviGeometryType.gviGeometryPoint:
                                                            IPoint pt = geo as IPoint;
                                                            IPOI poi = (new GeometryFactoryClass()).CreateGeometry(gviGeometryType.gviGeometryPOI, gviVertexAttribute.gviVertexAttributeZ) as IPOI;
                                                            poi.X = pt.X;
                                                            poi.Y = pt.Y;
                                                            poi.Z = pt.Z + 2;
                                                            totalx += poi.X;
                                                            totaly += poi.Y;
                                                            totalz += poi.Z;
                                                            poi.ImageName =Path.Combine(Application.StartupPath, "..\\Resource\\Images\\POI\\valvePre.png");
                                                            poi.Size = SystemInfo.Instance.SymbolSize;
                                                            poi.ShowName = false;
                                                            IRenderPOI rpoi = app.Current3DMapControl.ObjectManager.CreateRenderPOI(poi);
                                                            this._listRGuid.Add(rpoi.Guid);
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
                            }
                        }
                        if (json["nextValveIds"] != null)
                        {
                            string nextValveIds = json["nextValveIds"].ToString();
                            string[] arr = nextValveIds.Split(';');
                            foreach (string item in arr)
                            {
                                int index = item.LastIndexOf("_");
                                string fcguid = item.Substring(0, index);
                                string oid = item.Substring(index + 1, item.Length - index - 1);
                                DF3DFeatureClass dffc1 = DF3DFeatureClassManager.Instance.GetFeatureClassByID(fcguid);
                                if (dffc != null)
                                {
                                    IFeatureClass fc1 = dffc1.GetFeatureClass();
                                    if (fc1 != null)
                                    {
                                        IFdeCursor cursor = null;
                                        IRowBuffer row = null;
                                        try
                                        {
                                            IQueryFilter filter = new QueryFilter();
                                            filter.WhereClause = "oid=" + oid;
                                            filter.SubFields = "oid,Shape";
                                            cursor = fc1.Search(filter, false);
                                            row = cursor.NextRow();
                                            if (row != null && !row.IsNull(1))
                                            {
                                                object obj = row.GetValue(1);
                                                if (obj is IGeometry)
                                                {
                                                    IGeometry geo = obj as IGeometry;
                                                    switch (geo.GeometryType)
                                                    {
                                                        case gviGeometryType.gviGeometryPoint:
                                                            IPoint pt = geo as IPoint;
                                                            IPOI poi = (new GeometryFactoryClass()).CreateGeometry(gviGeometryType.gviGeometryPOI, gviVertexAttribute.gviVertexAttributeZ) as IPOI;
                                                            poi.X = pt.X;
                                                            poi.Y = pt.Y;
                                                            poi.Z = pt.Z + 2;
                                                            totalx += poi.X;
                                                            totaly += poi.Y;
                                                            totalz += poi.Z;
                                                            poi.ImageName = Path.Combine(Application.StartupPath, "..\\Resource\\Images\\POI\\valveNext.png");
                                                            poi.Size = SystemInfo.Instance.SymbolSize;
                                                            poi.ShowName = false;
                                                            IRenderPOI rpoi = app.Current3DMapControl.ObjectManager.CreateRenderPOI(poi);
                                                            this._listRGuid.Add(rpoi.Guid);
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
                            }
                        }
                        #endregion

                        // 定位
                        IVector3 vect ;
                        IEulerAngle ang;
                        app.Current3DMapControl.Camera.GetCamera(out vect,out ang);
                        vect.Set(totalx / count, totaly / count, totalz / count + 600.0);
                        ang.Set(ang.Heading, -90, ang.Roll);
                        app.Current3DMapControl.Camera.SetCamera(vect, ang, gviSetCameraFlags.gviSetCameraNoFlags);
                    }
                }
                else
                {
                    XtraMessageBox.Show("分析出错", "提示");
                    return;
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                WaitForm.Stop();
            }
        }

        private string GetValveClosed(string fcGuid, string oid)
        {
            try
            {
                var reg = FacilityInfoService.GetFacClassRegByFeatureClassID(fcGuid);
                if (reg == null || reg.FacilityType.Name != "PipeLine")
                {
                    return "{'res':false}";
                }
                var tc = FacilityInfoService.GetTopoClassByFacClassCode(reg.FacClassCode);
                if (tc == null)
                {
                    return "{'res':false}";
                }
                var net = tc.GetNetwork();
                if (net != null)
                {
                    var e = EdgeManager.Instance.GetEdgeByID(fcGuid, oid);
                    if (e == null)
                    {
                        return "{'res':false}";
                    }
                    var startId = e.PreNode.ID;
                    var endId = e.NextNode.ID;
                    if (string.IsNullOrEmpty(startId) || string.IsNullOrEmpty(endId))
                    {
                        return "{'res':false}";
                    }
                    var valveIds = FacilityInfoService.GetValveIdsByFCGuid(startId.Substring(0, startId.LastIndexOf("_")));
                    if (valveIds == null || valveIds.Count == 0)
                    {
                        return "{'res':true,'msg':'该管道不存在阀门。'}";
                    }
                    var recordPre = new HashSet<string>();
                    var recordNext = new HashSet<string>();
                    net.BGFX(startId, endId, valveIds, ref recordPre, ref recordNext);
                    if (recordPre.Count == 0 && recordNext.Count == 0)
                    {
                        return "{'res':true,'msg':'未搜索到要关闭的阀门。'}";
                    }
                    var res = "{'res':true,'msg':'搜索到要关闭的阀门。','count':" + (recordPre.Count + recordNext.Count) + ",";
                    if (recordPre.Count != 0)
                    {
                        res += "'preValveIds':'";

                        foreach (string str in recordPre)
                        {
                            res += str + ";";
                        }
                        res = res.Substring(0, res.Length - 1) + "'";
                    }
                    if (recordNext.Count != 0)
                    {
                        if (recordPre.Count != 0)
                        {
                            res += ",";
                        }
                        res += "'nextValveIds':'";
                        foreach (string str in recordNext)
                        {
                            res += str + ";";
                        }
                        res = res.Substring(0, res.Length - 1) + "'";
                    }
                    res += "}";
                    return res;
                }
                else
                {
                    return "{'res':false}";
                }
            }
            catch (Exception ex)
            {
                return "{'res':false}";
            }
        }
    }
}
