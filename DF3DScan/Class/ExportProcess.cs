using DevExpress.XtraEditors;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using Gvitech.CityMaker.Controls;
using Gvitech.CityMaker.RenderControl;
using System;
using System.Collections;
using System.Threading;
using ICSharpCode.Core;
using DF3DScan.Frm;
using DF3DControl.Base;
namespace DF3DScan.Class
{
    public class ExportProcess
    {
        private static ExportProcess _exportProcess;
        private double time;
        private ProgressDialog _dlg;
        private ICameraTour _tour;
        private System.Collections.ArrayList _groupNodeToPlay;
        private string _fileName = string.Empty;
        private int _vindex = -1;
        private string _fileType = string.Empty;
        private int _fps = -1;
        private TreeList _treelist;
        private TreeListNode _lastNodeInGroup;
        private AxRenderControl d3;

        private ExportProcess()
        {
            d3 = DF3DApplication.Application.Current3DMapControl;
        }
        public static ExportProcess Instance()
        {
            if (ExportProcess._exportProcess == null)
            {
                ExportProcess._exportProcess = new ExportProcess();
            }
            return ExportProcess._exportProcess;
        }

        // 注册输出视频事件
        public void AdviseEvent(ICameraTour tour, System.Collections.ArrayList list, string fileName, int vindex, string fileType, int fps, TreeList treelist, TreeListNode lastNodeInGroup)
        {
            this.UnAdviseEvent();
            d3.RcVideoExportBegin += new Gvitech.CityMaker.Controls._IRenderControlEvents_RcVideoExportBeginEventHandler(this.AxRenderControl_RcVideoExportBegin);
            d3.RcVideoExporting += new Gvitech.CityMaker.Controls._IRenderControlEvents_RcVideoExportingEventHandler(this.AxRenderControl_RcVideoExporting);
            d3.RcVideoExportEnd += new Gvitech.CityMaker.Controls._IRenderControlEvents_RcVideoExportEndEventHandler(this.AxRenderControl_RcVideoExportEnd);
            this._tour = tour;
            this._groupNodeToPlay = list;
            this._fileName = fileName;
            this._vindex = vindex;
            this._fileType = fileType;
            this._fps = fps;
            this._treelist = treelist;
            this._lastNodeInGroup = lastNodeInGroup;
        }
        private void UnAdviseEvent()
        {
            d3.RcVideoExportBegin -= new Gvitech.CityMaker.Controls._IRenderControlEvents_RcVideoExportBeginEventHandler(this.AxRenderControl_RcVideoExportBegin);
            d3.RcVideoExporting -= new Gvitech.CityMaker.Controls._IRenderControlEvents_RcVideoExportingEventHandler(this.AxRenderControl_RcVideoExporting);
            d3.RcVideoExportEnd -= new Gvitech.CityMaker.Controls._IRenderControlEvents_RcVideoExportEndEventHandler(this.AxRenderControl_RcVideoExportEnd);
        }

        // 输出视频结束
        private void AxRenderControl_RcVideoExportEnd(object sender, _IRenderControlEvents_RcVideoExportEndEvent e)
        {
            if (e.isAborted)
            {
                XtraMessageBox.Show("输出取消！","提示");
                this._dlg.Close();
                this.UnAdviseEvent();
                return;
            }
            if (this._groupNodeToPlay == null)
            {
                this.time = e.time;
                this.time /= 1000.0;
                this.time = System.Math.Round(this.time, 2);
                XtraMessageBox.Show("输出完成，用时" + this.time + "秒。", "提示");
                this._dlg.Close();
                this.UnAdviseEvent();
                return;
            }
            this._dlg.Close();
            this.time += e.time;
            this.time /= 1000.0;
            this._groupNodeToPlay.RemoveAt(0);
            if (this._groupNodeToPlay.Count > 0)
            {
                TreeListNode treeListNode = this._groupNodeToPlay[0] as TreeListNode;
                ICameraTour cameraTour = treeListNode.GetValue("tl_Object") as ICameraTour;
                this._vindex++;
                cameraTour.ExportVideo(this._fileName + "_" + this._vindex.ToString() + this._fileType, this._fps);
                this._treelist.SetFocusedNode(treeListNode);
                return;
            }
            if (this._groupNodeToPlay.Count == 0)
            {
                this._treelist.SetFocusedNode(this._lastNodeInGroup);
                this.time = System.Math.Round(this.time, 2);
                XtraMessageBox.Show("输出完成，用时" + this.time + "秒。", "提示");
                this._dlg.Close();
                this.UnAdviseEvent();
            }
        }

        // 输出视频过程中
        private void AxRenderControl_RcVideoExporting(object sender, _IRenderControlEvents_RcVideoExportingEvent e)
        {
            System.Threading.Thread.Sleep(10);
            this._dlg.labelTooltip.Text = "正在进行中,请稍后...";
            this._dlg.progressBarControl.EditValue = 100f * e.percentage;
        }

        // 开始输出视频
        private void AxRenderControl_RcVideoExportBegin(object sender, _IRenderControlEvents_RcVideoExportBeginEvent e)
        {
            this._dlg = new ProgressDialog(this._tour);
            this._dlg.ShowDialog();
        }
    }
}
