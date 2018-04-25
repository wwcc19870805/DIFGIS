using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraEditors;
using DevExpress.XtraBars.Docking;
using System.Drawing;
using DF3DDraw;
using DF3DControl.Command;
using DFDataConfig.Logic;
using Gvitech.CityMaker.FdeCore;
using Gvitech.CityMaker.FdeGeometry;
using DF3DData.Class;
using DFDataConfig.Class;
using DFWinForms.Class;
using DF3DPipe.Query.Frm;
using DF3DPipe.Query.UC;

namespace DF3DPipe.Query.Command
{
    class CmdRegionConditionQuery : AbstractMap3DCommand
    {
        private DrawTool _drawTool;

        public override void Run(object sender, EventArgs e)
        {
            Map3DCommandManager.Push(this);
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
        public void Clear()
        {
            if (this._drawTool != null)
            {
                this._drawTool.Close();
            }

        }
        private void OnStartDraw()
        {
            if (this._drawTool != null)
            {
                Clear();
            }
        }
        private DockPanel _dockPanel;
        private int _height = 400;
        private UIDockPanel _uPanel;
        private int _width = 225;
        private UCPropertyInfo _uc;
        private System.Drawing.Point Location
        {
            get
            {
                int width = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width;
                int height = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height;
                return new System.Drawing.Point((width - this._width) / 2, (height - this._height) / 2);
            }
        }
        private void OnFinishedDraw()
        {
            if (this._drawTool != null && this._drawTool.GeoType == DrawType.Polygon && this._drawTool.GetGeo() != null)
            {
                try
                {
                    WaitForm.Start("正在空间查询...", "请稍后");
                    bHaveRes = false;
                    RegionAnalysis();
                    if (!bHaveRes)
                    {
                        WaitForm.Stop();
                        XtraMessageBox.Show("空间查询为空！", "提示");
                        return;
                    }
                    WaitForm.Stop();
                    FrmCompoundConditionQuery dialog = new FrmCompoundConditionQuery();
                    dialog.SetData(resRootLogicGroups, resRootMajorClasses, this._drawTool.GetGeo());
                    if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        this._uPanel = new UIDockPanel("查询结果", "查询结果", this.Location, this._width, this._height);
                        this._dockPanel = FloatPanelManager.Instance.Add(ref this._uPanel, DockingStyle.Right);
                        this._dockPanel.Visibility = DockVisibility.Visible;
                        this._dockPanel.FloatSize = new System.Drawing.Size(this._width, this._height);
                        this._dockPanel.Width = this._width;
                        this._dockPanel.Height = this._height;
                        this._uc = new UCPropertyInfo();
                        this._uc.Init();
                        this._uc.Dock = System.Windows.Forms.DockStyle.Fill;
                        this._uPanel.RegisterEvent(new PanelClose(this.Close));
                        this._dockPanel.Controls.Add(this._uc);
                        this._uc.SetPropertyInfo(dialog.Dict, dialog.FacType, this._drawTool.GetGeo().Clone2(gviVertexAttribute.gviVertexAttributeNone));
                    }

                }
                catch(Exception ex)
                {
                    WaitForm.Stop();
                } 
            }
        }
        private void Close()
        {
            this.RestoreEnv();
        }

        private List<LogicGroup> resRootLogicGroups = new List<LogicGroup>();
        private List<MajorClass> resRootMajorClasses = new List<MajorClass>();
        private bool bHaveRes = false;
        private void ClassQuery(MajorClass mc, MajorClass mctemp, ISpatialFilter filter, IGeometry geo)
        {
            if (mc == null || filter == null || geo == null) return;
            string[] arrFc3DId = mc.Fc3D.Split(';');
            if (arrFc3DId == null) return;
            foreach (SubClass sc in mc.SubClasses)
            {
                if (!sc.Visible3D) continue;
                bool bHave = false;
                foreach (string fc3DId in arrFc3DId)
                {
                    DF3DFeatureClass dffc = DF3DFeatureClassManager.Instance.GetFeatureClassByID(fc3DId);
                    if (dffc == null) continue;
                    FacilityClass facc = dffc.GetFacilityClass();
                    IFeatureClass fc = dffc.GetFeatureClass();
                    if (fc == null || facc == null) continue;

                    filter.WhereClause = "GroupId = " + sc.GroupId;
                    int count = fc.GetCount(filter);
                    if (count == 0) continue;
                    bHave = true;
                    bHaveRes = true;
                   
                    if (bHave) break;
                }
                if (bHave)
                {
                    SubClass sctemp = new SubClass(sc.Name, sc.GroupId, sc.Parent);
                    sctemp.Visible3D = sc.Visible3D;
                    mctemp.SubClasses.Add(sctemp);
                }
            }
        }

        private void RecursiveRegionAnalysis(List<LogicGroup> listlg, LogicGroup lgtemp, ISpatialFilter filter, IGeometry geo)
        {
            if (listlg == null) return;
            foreach (LogicGroup lg in listlg)
            {
                LogicGroup lgtemp1 = new LogicGroup(lg.Name, lg.Alias, lg.Parent);
                lgtemp.LogicGroups.Add(lgtemp1);
                RecursiveRegionAnalysis(lg.LogicGroups, lgtemp1, filter, geo);
                foreach (MajorClass mc in lg.MajorClasses)
                {
                    MajorClass mctemp = new MajorClass(mc.Name, mc.Alias, mc.ClassifyField, mc.Fc2D, mc.Fc3D, mc.Parent);
                    ClassQuery(mc, mctemp, filter, geo);
                    lgtemp.MajorClasses.Add(mctemp);
                }
            }
        }

        private void RegionAnalysis()
        {
            resRootLogicGroups.Clear();
            resRootMajorClasses.Clear();

            IGeometry geo = this._drawTool.GetGeo().Clone2(gviVertexAttribute.gviVertexAttributeNone);
            if (geo == null) return;
            ISpatialFilter filter = new SpatialFilter();
            filter.Geometry = geo;
            filter.SpatialRel = gviSpatialRel.gviSpatialRelContains;
            filter.GeometryField = "FootPrint";

            foreach (LogicGroup lg in LogicDataStructureManage3D.Instance.RootLogicGroups)
            {
                LogicGroup lgtemp = new LogicGroup(lg.Name, lg.Alias, lg.Parent);
                resRootLogicGroups.Add(lgtemp);

                RecursiveRegionAnalysis(lg.LogicGroups, lgtemp, filter, geo);
                foreach (MajorClass mc in lg.MajorClasses)
                {
                    MajorClass mctemp = new MajorClass(mc.Name, mc.Alias, mc.ClassifyField, mc.Fc2D, mc.Fc3D, mc.Parent);
                    lgtemp.MajorClasses.Add(mctemp);
                    ClassQuery(mc, mctemp, filter, geo);
                }
            }
            foreach (MajorClass mc in LogicDataStructureManage3D.Instance.RootMajorClasses)
            {
                MajorClass mctemp = new MajorClass(mc.Name, mc.Alias, mc.ClassifyField, mc.Fc2D, mc.Fc3D, mc.Parent);
                resRootMajorClasses.Add(mctemp);
                ClassQuery(mc, mctemp, filter, geo);
            }
        }

    }
}
