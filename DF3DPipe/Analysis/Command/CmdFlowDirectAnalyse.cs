using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DF3DControl.Command;
using DF3DControl.UserControl.View;
using DFWinForms.Service;
using DF3DControl.Base;
using Gvitech.CityMaker.RenderControl;
using DF3DData.Class;
using Gvitech.CityMaker.FdeCore;
using DFDataConfig.Class;
using Gvitech.CityMaker.FdeGeometry;
using System.Windows.Forms;
using System.IO;
using Gvitech.CityMaker.Resource;
using DF3DPipeCreateTool.Class;
using DF3DPipeCreateTool.Service;
using DF3DAuthority;
using DevExpress.XtraEditors;
using DF3DPipeCreateTool.ParamModeling;
using Gvitech.CityMaker.Math;
using DFCommon.Class;
using DFWinForms.Class;

namespace DF3DPipe.Analysis.Command
{
    public class CmdFlowDirectAnalyse: AbstractMap3DCommand
    {
        private bool _isAuth;
        private List<Guid> _listRender;

        public override void Run(object sender, System.EventArgs e)
        {
            _isAuth = Authority3DService.Instance.IsAuthorized;
            //if (!_isAuth)
            //{
            //    XtraMessageBox.Show("此功能需要USB Key。", "提示");
            //    return;
            //}

            Map3DCommandManager.Push(this);
            IMap3DView map3DView = UCService.GetContent(typeof(Map3DView)) as Map3DView;
            if (map3DView == null) return;
            bool b3DBind = map3DView.Bind(this);
            if (!b3DBind) return;
            if (DF3DApplication.Application != null && DF3DApplication.Application.Current3DMapControl != null)
            {
                DF3DApplication.Application.Current3DMapControl.InteractMode = Gvitech.CityMaker.RenderControl.gviInteractMode.gviInteractSelect;
                DF3DApplication.Application.Current3DMapControl.MouseSelectObjectMask = Gvitech.CityMaker.RenderControl.gviMouseSelectObjectMask.gviSelectFeatureLayer;
                DF3DApplication.Application.Current3DMapControl.MouseSelectMode = Gvitech.CityMaker.RenderControl.gviMouseSelectMode.gviMouseSelectClick | Gvitech.CityMaker.RenderControl.gviMouseSelectMode.gviMouseSelectDrag;
                this._listRender = new List<Guid>();
            }
        }

        public override void RestoreEnv()
        {
            //if (!_isAuth) return;

            IMap3DView map3DView = UCService.GetContent(typeof(Map3DView)) as Map3DView;
            if (map3DView == null) return;
            map3DView.UnBind(this);
            if (DF3DApplication.Application != null && DF3DApplication.Application.Current3DMapControl != null)
            {
                DF3DApplication.Application.Current3DMapControl.InteractMode = Gvitech.CityMaker.RenderControl.gviInteractMode.gviInteractNormal;
                DF3DApplication.Application.Current3DMapControl.MouseSelectObjectMask = Gvitech.CityMaker.RenderControl.gviMouseSelectObjectMask.gviSelectAll;
                DF3DApplication.Application.Current3DMapControl.MouseSelectMode = Gvitech.CityMaker.RenderControl.gviMouseSelectMode.gviMouseSelectClick;
                foreach (Guid g in this._listRender)
                {
                    DF3DApplication.Application.Current3DMapControl.ObjectManager.DeleteObject(g);
                }
                this._listRender.Clear();
            }
            Map3DCommandManager.Pop();
        }

        private void ShowFlowDirect(IFeatureLayerPickResult pr)
        {
            if (pr == null) return;
            IRowBuffer row = null;
            IFdeCursor cursor = null;
            try
            {
                int fid = pr.FeatureId;
                DF3DFeatureClass dffc = DF3DFeatureClassManager.Instance.GetFeatureClassByID(pr.FeatureLayer.FeatureClassId.ToString());
                if (dffc == null) return;
                IFeatureClass fc = dffc.GetFeatureClass();
                FacilityClass fac = dffc.GetFacilityClass();
                if (fc == null || fac == null || fac.Name != "PipeLine") return;
                DFDataConfig.Class.FieldInfo fi = fac.GetFieldInfoBySystemName("FlowDirect");
                if (fi == null) return;
                IFieldInfoCollection fiCol = fc.GetFields();
                int index = fiCol.IndexOf(fi.Name);
                if (index == -1) return;
                int indexShape = fiCol.IndexOf("Shape");

                IQueryFilter filter = new QueryFilter();
                filter.WhereClause = "oid=" + fid;
                cursor = fc.Search(filter, false);
                row = cursor.NextRow();
                if (row == null) return;

                IPolyline line = null;
                if (indexShape != -1 && !row.IsNull(indexShape))
                {
                    object obj = row.GetValue(indexShape);
                    if (obj != null && obj is IPolyline) line = obj as IPolyline;
                }
                string flowDirectValue = "0";
                int type = 1;// 1表示具有流向字段；2表示从高到低
                if (!row.IsNull(index))
                {
                    flowDirectValue = row.GetValue(index).ToString();
                    if (flowDirectValue != "0" && flowDirectValue != "1") flowDirectValue = "0";
                    type = 1;
                }
                else if (line != null)
                {
                    if (line.StartPoint.Z > line.EndPoint.Z) flowDirectValue = "0";
                    else flowDirectValue = "1";
                    type = 2;
                }
                if (line == null) return;

                if (this._isAuth)
                {
                    #region 流向渲染1
                    FacClassReg reg = FacilityInfoService.GetFacClassRegByFeatureClassID(fc.Guid.ToString());
                    if (reg == null) return;
                    TopoClass tc = FacilityInfoService.GetTopoClassByFacClassCode(reg.FacClassCode);
                    if (tc == null) return;
                    FacStyleClass style = null;
                    List<FacStyleClass> facilityStyles = FacilityInfoService.GetFacStyleByFacClassCode(reg.FacClassCode);
                    if ((facilityStyles != null) && (facilityStyles.Count > 0))
                    {
                        style = facilityStyles[0];
                    }
                    PipeLineFac plfac = new PipeLineFac(reg, style, row, tc);
                    IRenderModelPoint rmp = null;
                    DrawGeometry.Ocx = DF3DApplication.Application.Current3DMapControl;
                    plfac.ShowFlowDirection(int.Parse(flowDirectValue), out rmp);
                    if (rmp != null) this._listRender.Add(rmp.Guid);
                    #endregion
                }
                else
                {
                    #region 流向渲染2
                    IEulerAngle angle = DF3DApplication.Application.Current3DMapControl.Camera.GetAimingAngles2(line.StartPoint, line.EndPoint);
                    double dia = 0.0;
                    string diaField = fac.GetFieldInfoNameBySystemName("Diameter");
                    int indexDia = fiCol.IndexOf(diaField);
                    if (indexDia != -1 && !row.IsNull(indexDia))
                    {
                        string diaStr = row.GetValue(indexDia).ToString();
                        int indexDia1 = diaStr.ToString().IndexOf('*');
                        if (indexDia1 != -1)
                        {
                            var dia1 = int.Parse(diaStr.ToString().Substring(0, indexDia1));
                            var dia2 = int.Parse(diaStr.ToString().Substring(indexDia1 + 1, diaStr.ToString().Length));
                            dia = ((dia1 > dia2) ? dia1 : dia2) * 0.001;
                        }
                        else
                        {
                            dia = int.Parse(diaStr) * 0.001;
                        }
                    }
                    List<IPoint> points = new List<IPoint>();
                    if (flowDirectValue == "0")
                    {
                        for (int i = 0; i < line.PointCount; i++)
                        {
                            IPoint pt = line.GetPoint(i);
                            pt.Z += dia / 2;
                            points.Add(pt);
                        }
                    }
                    else if (flowDirectValue == "1")
                    {
                        for (int i = line.PointCount - 1; i >= 0; i--)
                        {
                            IPoint pt = line.GetPoint(i);
                            pt.Z += dia / 2;
                            points.Add(pt);
                        }
                    }
                    for (int i = 0; i < points.Count - 1; i++)
                    {
                        IPoint pt1 = points[i];
                        var pt2 = points[i + 1];

                        double delt = 0;
                        double _rate, _distance;
                        if (!(Math.Abs(dia) < 0.0000001))
                        {
                            delt = 2.0 * dia;
                            if (dia <= 0.5 && dia >= 0.3)
                            {
                                _rate = 7 * dia;
                                _distance = 3 * dia / 0.4;
                            }
                            else if (dia < 0.3 && dia > 0.1)
                            {
                                _rate = 12 * dia;
                                _distance = 6 * dia / 0.4;
                            }
                            else if (dia <= 0.1)
                            {
                                _rate = 22 * dia;
                                _distance = 9 * dia / 0.4;
                            }
                            else
                            {
                                _rate = 3.5 * dia;
                                _distance = 1.5 * dia / 0.4;
                            }
                        }
                        else
                        {
                            _rate = 2.0;
                            _distance = 3.0;
                            //z = maxZ + 0.2;
                            delt = 0.2;
                        }
                        List<IPoint> list = DisPerseLine(pt1, pt2, _distance);
                        if (list.Count < 2) return;
                        List<IPoint> list1 = new List<IPoint>();
                        IGeometryFactory geoFact = new GeometryFactoryClass();
                        for (int j = 0; j < list.Count - 1; j++)
                        {
                            IPoint p = geoFact.CreatePoint(gviVertexAttribute.gviVertexAttributeZ);
                            p.X = (list[j].X + list[j + 1].X) / 2;
                            p.Y = (list[j].Y + list[j + 1].Y) / 2;
                            p.Z = (list[j].Z + list[j + 1].Z) / 2;
                            list1.Add(p);
                        }

                        for (var m = 0; m < list1.Count; m++)
                        {
                            IPosition pos = new PositionClass();
                            pos.X = list1[m].X;
                            pos.Y = list1[m].Y;
                            pos.Altitude = list1[m].Z + delt;
                            pos.AltitudeType = gviAltitudeType.gviAltitudeTerrainAbsolute;
                            pos.Heading = angle.Heading;
                            pos.Tilt = angle.Tilt;
                            pos.Roll = angle.Roll;
                            UInt32 color;
                            if (type == 1) color = 0xFFFFFF00;
                            else color = 0xFF00FFFF;
                            ITerrainArrow rArrow = DF3DApplication.Application.Current3DMapControl.ObjectManager.CreateArrow(pos, 0.8 * _rate, 3, color, color, DF3DApplication.Application.Current3DMapControl.ProjectTree.RootID);
                            this._listRender.Add(rArrow.Guid);
                        }
                    }
                    #endregion
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

        private List<IPoint> DisPerseLine(IPoint pStart, IPoint pEnd, double disperse)
        {
            if (pStart == null || pEnd == null) return null;
            List<IPoint> result = new List<IPoint>();
            IVector3 vector = new Vector3();
            vector.Set(pStart.X, pStart.Y, pStart.Z);
            IVector3 vector2 = new Vector3();
            vector2.Set(pEnd.X - pStart.X, pEnd.Y - pStart.Y, pEnd.Z - pStart.Z);
            double length = vector2.Length;
            int num = int.Parse(Math.Floor(length / disperse).ToString());
            IGeometryFactory geoFact = new GeometryFactoryClass();
            for (var i = 0; i <= num; i++)
            {
                var gvitechPoint = geoFact.CreatePoint(gviVertexAttribute.gviVertexAttributeZ);
                if (i == 0)
                {
                    gvitechPoint.X = pStart.X;
                    gvitechPoint.Y = pStart.Y;
                    gvitechPoint.Z = pStart.Z;
                }
                else
                {
                    if (i == num)
                    {
                        gvitechPoint.X = pEnd.X;
                        gvitechPoint.Y = pEnd.Y;
                        gvitechPoint.Z = pEnd.Z;
                    }
                    else
                    {
                        gvitechPoint.X = vector.X + vector2.X * i / num;
                        gvitechPoint.Y = vector.Y + vector2.Y * i / num;
                        gvitechPoint.Z = vector.Z + vector2.Z * i / num;
                    }
                }
                result.Add(gvitechPoint);
            }
            return result;
        }

        public override void RcMouseClickSelect(object sender, Gvitech.CityMaker.Controls._IRenderControlEvents_RcMouseClickSelectEvent e)
        {
            if (e.intersectPoint == null || e.pickResult == null || e.pickResult.Type != Gvitech.CityMaker.RenderControl.gviObjectType.gviObjectFeatureLayer) return;
            ShowFlowDirect(e.pickResult as IFeatureLayerPickResult);
        }

        public override void RcMouseDragSelect(object sender, Gvitech.CityMaker.Controls._IRenderControlEvents_RcMouseDragSelectEvent e)
        {
            if (e.pickResults == null || e.pickResults.Count == 0) return;
            WaitForm.Start("正在渲染流向...", "请稍后");
            for (int i = 0; i < e.pickResults.Count; i++)
            {
                IPickResult pr = e.pickResults[i];
                if (pr == null || pr.Type != gviObjectType.gviObjectFeatureLayer) continue;
                ShowFlowDirect(pr as IFeatureLayerPickResult);
            }
            WaitForm.Stop();
        }
    }
}
