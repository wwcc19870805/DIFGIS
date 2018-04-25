using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraEditors;
using System.Drawing;
using DF3DDraw;
using DF3DControl.Command;
using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.FdeCore;
using System.Data;
using DFDataConfig.Logic;
using DFDataConfig.Class;
using DF3DData.Class;
using DFWinForms.Class;
using DF3DPipe.Stats.Frm;

namespace DF3DPipe.Stats.Command
{
    public class CmdRegionPipeNodeNumberStats : AbstractMap3DCommand
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
                    WaitForm.Start("正在统计...", "请稍后");
                    DataTable dtResult = RegionAnalysis();
                    if (dtResult == null || dtResult.Rows.Count == 0)
                    {
                        WaitForm.Stop();
                        XtraMessageBox.Show("统计结果为空！", "提示");
                        return;
                    }
                    WaitForm.Stop();
                    FrmPipeNodeStatsOutput dialog = new FrmPipeNodeStatsOutput();
                    dialog.SetData1(dtResult);
                    dialog.ShowDialog();
                }
                catch
                {
                    WaitForm.Stop();
                }
            }
        }

        private DataTable RegionAnalysis()
        {
            IGeometry geo = this._drawTool.GetGeo();
            if (geo == null) return null;
            ISpatialFilter filter = new SpatialFilter();
            filter.Geometry = geo;
            filter.SpatialRel = gviSpatialRel.gviSpatialRelEnvelope;
            filter.GeometryField = "Geometry";
            DataTable dtResult = new DataTable();
            dtResult.Columns.AddRange(new DataColumn[]{new DataColumn("PIPENODETYPE"),new DataColumn("PVALUE"),
            new DataColumn("NUMBER",typeof(long)),new DataColumn("TOTALNUMBER",typeof(long))});
            foreach (MajorClass mc in LogicDataStructureManage3D.Instance.GetAllMajorClass())
            {
                string[] arrFc3DId = mc.Fc3D.Split(';');
                if (arrFc3DId == null) continue;
                long majorclasscount = 0;
                int indexStart = dtResult.Rows.Count;
                foreach (SubClass sc in mc.SubClasses)
                {
                    if (!sc.Visible3D) continue;
                    long sccount = 0;
                    bool bHave = false;
                    foreach (string fc3DId in arrFc3DId)
                    {
                        DF3DFeatureClass dffc = DF3DFeatureClassManager.Instance.GetFeatureClassByID(fc3DId);
                        if (dffc == null) continue;
                        FacilityClass facc = dffc.GetFacilityClass();
                        IFeatureClass fc = dffc.GetFeatureClass();
                        if (fc == null || facc == null || facc.Name != "PipeNode") continue;
                        int geometryIndex = fc.GetFields().IndexOf("Geometry");
                        if (geometryIndex == -1) continue;
                        filter.WhereClause = "GroupId = " + sc.GroupId;
                        filter.SubFields = "Geometry";

                        int count = fc.GetCount(filter);
                        if (count == 0) continue;
                        int loop = (int)Math.Ceiling(count / 800.0);
                        for (int k = 1; k <= loop; k++)
                        {
                            if (k == 1)
                            {
                                filter.ResultBeginIndex = 0;
                            }
                            else
                            {
                                filter.ResultBeginIndex = (k - 1) * 800;
                            }
                            filter.ResultLimit = 800;
                            IFdeCursor cursor = fc.Search(filter, true);
                            IRowBuffer row = null;
                            while ((row = cursor.NextRow()) != null)
                            {
                                IModelPoint modelPoint = row.GetValue(0) as IModelPoint;
                                if ((geo as IRelationalOperator2D).Contains2D(modelPoint))
                                {
                                    sccount++;
                                    bHave = true;
                                }
                            }
                        }
                    }
                    if (bHave)
                    {
                        DataRow dr = dtResult.NewRow();
                        dr["PIPENODETYPE"] = mc;
                        dr["PVALUE"] = sc;
                        dr["NUMBER"] = sccount;
                        majorclasscount += sccount;
                        dtResult.Rows.Add(dr);
                    }
                }
                int indexEnd = dtResult.Rows.Count;
                for (int i = indexStart; i < indexEnd; i++)
                {
                    DataRow dr = dtResult.Rows[i];
                    dr["TOTALNUMBER"] = majorclasscount;
                }
            }
            return dtResult;
        }


    }
}
