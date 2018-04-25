using System;
using System.Collections.Generic;
using System.Linq;
using ICSharpCode.Core;
using DF3DControl.Command;
using DFWinForms.UserControl;
using DF3DControl.Base;
using Gvitech.CityMaker.Math;
using DevExpress.XtraEditors;
using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.RenderControl;
using DFWinForms.Base;

namespace DF3DControl.UserControl.View
{
    public class Map3DView : BaseViewContent, IMap3DView
    {
        private Gvitech.CityMaker.Controls.AxRenderControl AxRenderControl3D;

        public Gvitech.CityMaker.Controls.AxRenderControl RenderControl3D
        {
            get
            {
                return AxRenderControl3D;
            }
        }

        public override bool IsActive
        {
            get
            {
                return base.IsActive;
            }
            set
            {
                if (value == true)
                {
                    this.Active();
                }
                else
                {
                    this.Deactive();
                }
                base.IsActive = value;
            }
        }

        public Map3DView()
        {
            InitializeComponent();
            DF3DApplication.Application.Current3DMapControl = this.AxRenderControl3D;
            this.AttachEvent();
        }

        private void AttachEvent()
        {
            this.AxRenderControl3D.RcMouseMove += new Gvitech.CityMaker.Controls._IRenderControlEvents_RcMouseMoveEventHandler(AxRenderControl3D_RcMouseMove);
            this.AxRenderControl3D.RcDataSourceDisconnected += new Gvitech.CityMaker.Controls._IRenderControlEvents_RcDataSourceDisconnectedEventHandler(AxRenderControl3D_RcDataSourceDisconnected);
            this.AxRenderControl3D.RcCameraUndoRedoStatusChanged += new EventHandler(AxRenderControl3D_RcCameraUndoRedoStatusChanged);
            this.AxRenderControl3D.MouseSelectMode = gviMouseSelectMode.gviMouseSelectMove;
            //添加可选择
            this.AxRenderControl3D.MouseSelectObjectMask = gviMouseSelectObjectMask.gviSelectAll;
        }

        private void DetachEvent()
        {
            this.AxRenderControl3D.RcMouseMove -= new Gvitech.CityMaker.Controls._IRenderControlEvents_RcMouseMoveEventHandler(AxRenderControl3D_RcMouseMove);
            this.AxRenderControl3D.RcDataSourceDisconnected -= new Gvitech.CityMaker.Controls._IRenderControlEvents_RcDataSourceDisconnectedEventHandler(AxRenderControl3D_RcDataSourceDisconnected);
            this.AxRenderControl3D.RcCameraUndoRedoStatusChanged -= new EventHandler(AxRenderControl3D_RcCameraUndoRedoStatusChanged);
            this.AxRenderControl3D.MouseSelectMode = gviMouseSelectMode.gviMouseSelectClick;
            //添加可选择
            this.AxRenderControl3D.MouseSelectObjectMask = gviMouseSelectObjectMask.gviSelectNone;
        }

        bool AxRenderControl3D_RcMouseMove(object sender, Gvitech.CityMaker.Controls._IRenderControlEvents_RcMouseMoveEvent e)
        {
            DF3DApplication app = DF3DApplication.Application;
            if (app == null || app.Workbench == null || app.Current3DMapControl == null) return false;
            int x = e.x;
            int y = e.y;
            IPoint pt;
            IPickResult pr = app.Current3DMapControl.Camera.ScreenToWorld(x, y, out pt);
            if (pr != null && pt != null)
            {
                string coord = String.Format("{0:#.###}  {1:#.###}  {2:#.###}m", pt.X, pt.Y, pt.Z);
                app.Workbench.SetOtherInfo(coord);
            }
            else app.Workbench.SetOtherInfo("");
            return false;
        }

        void AxRenderControl3D_RcCameraUndoRedoStatusChanged(object sender, EventArgs e)
        {
            DF3DApplication app = DF3DApplication.Application;
            if (app == null || app.Workbench == null) return;
            app.Workbench.UpdateMenu();
        }

        void AxRenderControl3D_RcDataSourceDisconnected(object sender, Gvitech.CityMaker.Controls._IRenderControlEvents_RcDataSourceDisconnectedEvent e)
        {
            string connectionInfo = e.connectionInfo;
            XtraMessageBox.Show("连接" + connectionInfo + "断开，请查看！", "提示");
        }

        private void InitializeComponent()
        {
            this.AxRenderControl3D = new Gvitech.CityMaker.Controls.AxRenderControl();
            ((System.ComponentModel.ISupportInitialize)(this.AxRenderControl3D)).BeginInit();
            this.SuspendLayout();
            // 
            // AxRenderControl3D
            // 
            this.AxRenderControl3D.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AxRenderControl3D.Enabled = true;
            this.AxRenderControl3D.Location = new System.Drawing.Point(0, 0);
            this.AxRenderControl3D.Name = "AxRenderControl3D";
            this.AxRenderControl3D.Size = new System.Drawing.Size(274, 287);
            this.AxRenderControl3D.TabIndex = 0;
            // 
            // Map3DView
            // 
            this.Controls.Add(this.AxRenderControl3D);
            this.Name = "Map3DView";
            this.Size = new System.Drawing.Size(274, 287);
            ((System.ComponentModel.ISupportInitialize)(this.AxRenderControl3D)).EndInit();
            this.ResumeLayout(false);

        }

        public override bool Bind(ICommand cmd)
        {
            if (cmd == null) return false;
            if (!(cmd is IMap3DCommand)) return false;
            IMap3DCommand map3DCommand = cmd as IMap3DCommand;
            this.AxRenderControl3D.RcMouseClickSelect += new Gvitech.CityMaker.Controls._IRenderControlEvents_RcMouseClickSelectEventHandler(map3DCommand.RcMouseClickSelect);
            this.AxRenderControl3D.RcMouseDragSelect += new Gvitech.CityMaker.Controls._IRenderControlEvents_RcMouseDragSelectEventHandler(map3DCommand.RcMouseDragSelect);
            this.AxRenderControl3D.RcLButtonUp += new Gvitech.CityMaker.Controls._IRenderControlEvents_RcLButtonUpEventHandler(map3DCommand.RcLButtonUp);
            this.AxRenderControl3D.RcMouseWheel += new Gvitech.CityMaker.Controls._IRenderControlEvents_RcMouseWheelEventHandler(map3DCommand.RcMouseWheel);
            this.AxRenderControl3D.RcMButtonUp += new Gvitech.CityMaker.Controls._IRenderControlEvents_RcMButtonUpEventHandler(map3DCommand.RcMButtonUp);
            this.AxRenderControl3D.RcCameraFlyFinished += new Gvitech.CityMaker.Controls._IRenderControlEvents_RcCameraFlyFinishedEventHandler(map3DCommand.RcCameraFlyFinished);
            this.AxRenderControl3D.RcRButtonUp += new Gvitech.CityMaker.Controls._IRenderControlEvents_RcRButtonUpEventHandler(map3DCommand.RcRButtonUp);
            this.AxRenderControl3D.RcKeyUp += new Gvitech.CityMaker.Controls._IRenderControlEvents_RcKeyUpEventHandler(map3DCommand.RcKeyUp);
            this.AxRenderControl3D.RcLButtonDblClk += new Gvitech.CityMaker.Controls._IRenderControlEvents_RcLButtonDblClkEventHandler(map3DCommand.RcLButtonDblClk);
            this.AxRenderControl3D.RcRButtonDblClk += new Gvitech.CityMaker.Controls._IRenderControlEvents_RcRButtonDblClkEventHandler(map3DCommand.RcRButtonDblClk);
            this.AxRenderControl3D.RcMButtonDblClk += new Gvitech.CityMaker.Controls._IRenderControlEvents_RcMButtonDblClkEventHandler(map3DCommand.RcMButtonDblClk);
            this.AxRenderControl3D.RcCameraUndoRedoStatusChanged += new EventHandler(map3DCommand.RcCameraUndoRedoStatusChanged);
            return true;
        }

        public override void UnBind(ICommand cmd)
        {
            if (cmd == null) return;
            if (!(cmd is IMap3DCommand)) return;

            IMap3DCommand map3DCommand = cmd as IMap3DCommand;
            this.AxRenderControl3D.RcMouseClickSelect -= new Gvitech.CityMaker.Controls._IRenderControlEvents_RcMouseClickSelectEventHandler(map3DCommand.RcMouseClickSelect);
            this.AxRenderControl3D.RcMouseDragSelect -= new Gvitech.CityMaker.Controls._IRenderControlEvents_RcMouseDragSelectEventHandler(map3DCommand.RcMouseDragSelect);
            this.AxRenderControl3D.RcLButtonUp -= new Gvitech.CityMaker.Controls._IRenderControlEvents_RcLButtonUpEventHandler(map3DCommand.RcLButtonUp);
            this.AxRenderControl3D.RcMouseWheel -= new Gvitech.CityMaker.Controls._IRenderControlEvents_RcMouseWheelEventHandler(map3DCommand.RcMouseWheel);
            this.AxRenderControl3D.RcMButtonUp -= new Gvitech.CityMaker.Controls._IRenderControlEvents_RcMButtonUpEventHandler(map3DCommand.RcMButtonUp);
            this.AxRenderControl3D.RcCameraFlyFinished -= new Gvitech.CityMaker.Controls._IRenderControlEvents_RcCameraFlyFinishedEventHandler(map3DCommand.RcCameraFlyFinished);
            this.AxRenderControl3D.RcRButtonUp -= new Gvitech.CityMaker.Controls._IRenderControlEvents_RcRButtonUpEventHandler(map3DCommand.RcRButtonUp);
            this.AxRenderControl3D.RcKeyUp -= new Gvitech.CityMaker.Controls._IRenderControlEvents_RcKeyUpEventHandler(map3DCommand.RcKeyUp);
            this.AxRenderControl3D.RcLButtonDblClk -= new Gvitech.CityMaker.Controls._IRenderControlEvents_RcLButtonDblClkEventHandler(map3DCommand.RcLButtonDblClk);
            this.AxRenderControl3D.RcRButtonDblClk -= new Gvitech.CityMaker.Controls._IRenderControlEvents_RcRButtonDblClkEventHandler(map3DCommand.RcRButtonDblClk);
            this.AxRenderControl3D.RcMButtonDblClk -= new Gvitech.CityMaker.Controls._IRenderControlEvents_RcMButtonDblClkEventHandler(map3DCommand.RcMButtonDblClk);
            this.AxRenderControl3D.RcCameraUndoRedoStatusChanged -= new EventHandler(map3DCommand.RcCameraUndoRedoStatusChanged);
        }

        public override void Activate()
        {
            base.Activate();
            DF3DApplication.Application.Workbench.SwitchToMenu(new string[] { "/Workbench/MainMenu/3D", "/Workbench/MainMenu" });
        }

        private void Active()
        {
            int docCount = DF3DApplication.Application.Workbench.GetDocumentCount();
            if (docCount == 2)
            {
                this.AxRenderControl3D.ResumeRendering();
            }
        }

        private void Deactive()
        {
            int docCount = DF3DApplication.Application.Workbench.GetDocumentCount();
            if (docCount == 2)
            {
                int docGroupCount = DF3DApplication.Application.Workbench.GetDocumentGroupCount();
                if (docGroupCount == 1)
                {
                    IContent c = DFApplication.Application.CurrentContent;
                    if(c is IViewContent && !(c is Map3DView))
                        this.AxRenderControl3D.PauseRendering(false);
                }
            }
        }
    }
}
