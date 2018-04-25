using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DF3DControl.Command;
using DF3DDraw;
using DFWinForms.Class;
using DFDataConfig.Logic;
using DFDataConfig.Class;
using DF3DData.Class;
using Gvitech.CityMaker.FdeCore;
using DevExpress.XtraEditors;
using DF3DPipe.Analysis.Frm;
using Gvitech.CityMaker.RenderControl;

namespace DF3DPipe.Analysis.Command
{
    public class CmdPipelineCross : AbstractMap3DCommand
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
        private void OnFinishedDraw()
        {
            if (this._drawTool != null && this._drawTool.GeoType == DrawType.Polygon && this._drawTool.GetGeo() != null)
            {
                try
                {
                    WaitForm.Start("正在空间查询...", "请稍后");

                    List<MajorClass> listMCs = LogicDataStructureManage3D.Instance.GetAllMajorClass();
                    if (listMCs != null)
                    {
                        ISpatialFilter filter = new SpatialFilterClass();
                        filter.Geometry = this._drawTool.GetGeo();
                        filter.GeometryField = "FootPrint";
                        filter.SpatialRel = gviSpatialRel.gviSpatialRelContains;
                        Dictionary<MajorClass, List<IRowBuffer>> dict = new Dictionary<MajorClass, List<IRowBuffer>>();
                        foreach (MajorClass mc in listMCs)
                        {
                            IRowBuffer row = null;
                            IFdeCursor cursor = null;
                            try
                            {
                                string[] arrFc3DId = mc.Fc3D.Split(';');
                                if (arrFc3DId == null) continue;
                                foreach (string fc3DId in arrFc3DId)
                                {
                                    DF3DFeatureClass dffc = DF3DFeatureClassManager.Instance.GetFeatureClassByID(fc3DId);
                                    if (dffc == null) continue;
                                    FacilityClass facc = dffc.GetFacilityClass();
                                    IFeatureClass fc = dffc.GetFeatureClass();
                                    IFeatureLayer fl = dffc.GetFeatureLayer();
                                    if (fl == null || fc == null || facc == null || facc.Name != "PipeLine") continue;
                                    if (fl.VisibleMask == gviViewportMask.gviViewNone) continue;
                                    int count = fc.GetCount(filter);
                                    if (count == 0) break;
                                    List<IRowBuffer> list = new List<IRowBuffer>();
                                    cursor = fc.Search(filter, false);
                                    while ((row = cursor.NextRow()) != null)
                                    {
                                        list.Add(row);
                                    }
                                    dict[mc] = list;
                                }
                            }
                            catch (Exception ex)
                            {
                            }
                            finally
                            {
                                if (row != null)
                                {
                                    System.Runtime.InteropServices.Marshal.ReleaseComObject(row);
                                    row = null;
                                }
                                if (cursor != null)
                                {
                                    System.Runtime.InteropServices.Marshal.ReleaseComObject(cursor);
                                    cursor = null;
                                }
                            }
                        }
                        WaitForm.Stop();
                        if (dict.Count == 0)
                        {
                            XtraMessageBox.Show("该区域内没有管线", "提示");
                            return;
                        }
                        FrmPipelineCross dlg = new FrmPipelineCross(dict);
                        dlg.Show();
                    }
                }
                catch (Exception ex)
                {
                    WaitForm.Stop();
                }
                finally
                {
                    
                }
            }
        }
    }
}
