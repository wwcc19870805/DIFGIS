/*----------------------------------------------------------------
			// Copyright (C) 2005 中冶集团武汉勘察研究院有限公司
			// 版权所有。 
			//
			// 文件名：CalculateDistance.cs
			// 文件功能描述：计算距离
			//
			// 
			// 创建标识：YuanHY 20060614
            // 操作步骤：1、点击命令按纽；
			//           2、在地图上点击鼠标；
			//			 3、双击、或点击右键\回车\空格键，弹出对话框;
			//           4、点击确定按钮,删除生成的地图元素。　　　　　　
			// 操作说明：ESC键 取消所有操作
　
			// 修改标识：Modify by YuanHY20081112
            // 修改描述：增加了ESC键的操作　    
----------------------------------------------------------------*/
using System;
using System.Windows.Forms;

using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.SystemUI;

using WSGRI.DigitalFactory.Commands;
using WSGRI.DigitalFactory.Gui.Views;
using WSGRI.DigitalFactory.Gui;
using WSGRI.DigitalFactory.Base;
using WSGRI.DigitalFactory.DFEditorLib;
using WSGRI.DigitalFactory.Services;
using WSGRI.DigitalFactory.DFFunction;

using ICSharpCode.Core.Services;

namespace WSGRI.DigitalFactory.DFEditorTool
{
    /// <summary>
    /// CalculateDistance 的摘要说明。
    /// </summary>
    public class CalculateDistance : AbstractMapCommand
    {
        private IDFApplication m_App;
        private IMapControl2 m_MapControl;
        private IMap m_FocusMap;
        private IActiveView m_pActiveView;
        private IMapView m_MapView = null;

        private IDisplayFeedback m_pFeedback;
        private INewLineFeedback m_pLineFeed;

        private bool m_bInUse;
        public static IPoint m_pPoint;
        public static IPoint m_pAnchorPoint;
        private IPoint m_pLastPoint;

        private IArray m_pRecordPointArray = new ArrayClass();
        private double m_dblDistance;
        private double m_dblTotalDistance;

        private IStatusBarService m_pStatusBarService;

        //private bool	isEnabled   = true;
        private string strCaption = "计算距离";
        private string strCategory = "工具";


        public CalculateDistance()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
            //获得状态栏的服务
            //m_pStatusBarService = (IStatusBarService)ServiceManager.Services.GetService(typeof(WSGRI.DigitalFactory.Services.UltraStatusBarService));

        }

        #region 类的属性
        //		public override bool IsEnabled 
        //		{
        //			get 
        //			{
        //				return isEnabled;
        //			}
        //			set 
        //			{
        //				isEnabled = value;
        //			}
        //		}

        public override string Caption
        {
            get
            {
                return strCaption;
            }
            set
            {
                strCaption = value;
            }
        }

        public override string Category
        {
            get
            {
                return strCategory;
            }
            set
            {
                strCategory = value;
            }
        }
        #endregion

        public override void Execute()
        {
            base.Execute();
            if (!(this.Hook is IDFApplication))
            {
                return;
            }
            else
            {
                m_App = (IDFApplication)this.Hook;
            }

            m_MapView = m_App.Workbench.GetView(typeof(MapView)) as IMapView;
            if (m_MapView == null)
            {
                return;
            }

            m_MapView.CurrentTool = this;

            m_MapControl = m_App.CurrentMapControl;
            m_FocusMap = m_MapControl.ActiveView.FocusMap;
            m_pActiveView = (IActiveView)this.m_FocusMap;
            m_pStatusBarService = m_App.StatusBarService;//获得状态服务

            CurrentTool.m_CurrentToolName = CurrentTool.CurrentToolName.CalculateDistance;

            CommonFunction.MapRefresh(m_pActiveView);

            //记录用户操作
            clsUserLog useLog = new clsUserLog();
            useLog.UserName = DFApplication.LoginUser;
            useLog.UserRoll = DFApplication.LoginSubSys;
            useLog.Operation = "测量距离";
            useLog.LogTime = System.DateTime.Now;
            useLog.TableLog = (m_App.CurrentWorkspace as IFeatureWorkspace).OpenTable("WSGRI_LOG");
            useLog.setUserLog();

        }

        public override void UnExecute()
        {
            m_MapControl.MousePointer = esriControlsMousePointer.esriPointerArrow;

        }

        public override void OnMouseDown(int button, int shift, int x, int y, double mapX, double mapY)
        {
            // TODO:  添加 DrawLine.OnMouseDown 实现
            base.OnMouseDown(button, shift, x, y, mapX, mapY);

            CalculateDistanceMouseDown(m_pAnchorPoint);

        }


        private void CalculateDistanceMouseDown(IPoint pPoint)
        {
            if (!m_bInUse)//如果命令没有使用
            {
                m_bInUse = true;

                m_pRecordPointArray.Add(pPoint);

                m_pLastPoint = pPoint;

                CommonFunction.DrawPointSMSSquareSymbol(m_MapControl, pPoint);

                m_pFeedback = new NewLineFeedbackClass();
                m_pLineFeed = (INewLineFeedback)m_pFeedback;
                m_pLineFeed.Start(pPoint);
                if (m_pFeedback != null) m_pFeedback.Display = m_pActiveView.ScreenDisplay;

            }
            else//若果命令正使用中
            {
                m_pLineFeed.Stop();
                m_pLineFeed.Start(pPoint);

                IPoint tempPoint = new PointClass();
                tempPoint.X = pPoint.X;
                tempPoint.Y = pPoint.Y;
                m_pRecordPointArray.Add(tempPoint);

                m_pLastPoint = tempPoint;

                m_dblDistance = CommonFunction.GetDistance_P12((IPoint)m_pRecordPointArray.get_Element(m_pRecordPointArray.Count - 2), m_pLastPoint);//最后一段的长度
                m_dblTotalDistance = m_dblTotalDistance + m_dblDistance;  //总长度			

                CommonFunction.DisplaypSegmentColToScreen(m_MapControl, ref m_pRecordPointArray);//可以刷新屏幕了

            }
        }

        public override void OnMouseMove(int button, int shift, int x, int y, double mapX, double mapY)
        {
            // TODO:  添加 DrawLine.OnMouseMove 实现
            base.OnMouseMove(button, shift, x, y, mapX, mapY);

            m_MapControl.MousePointer = esriControlsMousePointer.esriPointerCrosshair;

            m_pPoint = m_pActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(x, y);

            m_pAnchorPoint = m_pPoint;
            //+++++++++++++开始捕捉+++++++++++++++++++++			
            bool flag = CommonFunction.Snap(m_MapControl, m_App.CurrentConfig.cfgSnapEnvironmentSet, (IGeometry)m_pLastPoint, m_pAnchorPoint);

            if (!m_bInUse) return;

            m_pFeedback.MoveTo(m_pAnchorPoint);

            //向系统的状态栏总转递信息：最后一段的长度
            m_dblDistance = CommonFunction.GetDistance_P12(m_pAnchorPoint, m_pLastPoint);

            m_pStatusBarService.SetStateMessage("最后一段的长度:" + m_dblDistance.ToString(".###") + "米，总长度：" + (m_dblTotalDistance + m_dblDistance).ToString(".###") + "米");

        }

        public override void OnDoubleClick(int button, int shift, int x, int y, double mapX, double mapY)
        {
            // TODO:  添加 DrawLine.OnDoubleClick 实现
            base.OnDoubleClick(button, shift, x, y, mapX, mapY);

            EndCalculateDistance();

        }

        //结束计算工作
        public void EndCalculateDistance()
        {
            IGeometry pGeom = null;
            IPolyline pPolyline;
            IPointCollection pPointCollection;
            System.Windows.Forms.DialogResult result;

            pPolyline = (IPolyline)CommonFunction.MadeSegmentCollection(ref m_pRecordPointArray);

            if (m_bInUse)
            {

                pPointCollection = (IPointCollection)pPolyline;
                if (pPointCollection.PointCount < 2)
                {
                    MessageBox.Show("线上必须有两个点!");
                }
                else
                {
                    pGeom = (IGeometry)pPointCollection;
                }

                CommonFunction.AddElement(m_MapControl, pGeom);//绘制地图元素

                result = MessageBox.Show("总长为:" + m_dblTotalDistance.ToString(".###") + "米", "距离计算", MessageBoxButtons.OK, MessageBoxIcon.Information);

                if (result == DialogResult.OK)
                {
                    Reset();//复位;
                }

            }
        }

        private void Reset()
        {
            m_bInUse = false;
            m_pRecordPointArray.RemoveAll();//清空回退数组 
            m_pLineFeed = null;
            m_dblDistance = 0;
            m_dblTotalDistance = 0;

            m_pActiveView.FocusMap.ClearSelection();
            m_pActiveView.GraphicsContainer.DeleteAllElements();//删除创建的地图元素
            m_pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, m_MapControl.ActiveView.Extent);//视图刷新		

            m_pStatusBarService.SetStateMessage("就绪");

        }

        public override void Stop()
        {
            //this.Reset();
            base.Stop();
        }

        public override void OnBeforeScreenDraw(int hdc)
        {
            // TODO:  添加 DrawLine.OnBeforeScreenDraw 实现
            base.OnBeforeScreenDraw(hdc);

            if (m_pRecordPointArray.Count != 0)
            {
                IPoint pStartPoint = new PointClass();
                IPoint pEndPoint = new PointClass();
                pStartPoint = (IPoint)m_pRecordPointArray.get_Element(0);
                pEndPoint = (IPoint)m_pRecordPointArray.get_Element(m_pRecordPointArray.Count - 1);

                if (m_pLineFeed != null) m_pLineFeed.MoveTo(pEndPoint);
            }
        }

        //键盘事件(定义的快捷键)
        public override void OnKeyDown(int keyCode, int shift)
        {
            // TODO:  添加 DrawLine.OnKeyDown 实现
            base.OnKeyDown(keyCode, shift);

            if ((keyCode == 69 || keyCode == 13 || keyCode == 32) && m_bInUse && m_pRecordPointArray.Count >= 2)//按E键、ENTER 键、SPACEBAR 键结束绘制
            {
                EndCalculateDistance();

                return;
            }

            if (keyCode == 27)//ESC 键，取消所有操作
            {
                Reset();

                this.Stop();
                WSGRI.DigitalFactory.Commands.ICommand command = DFApplication.Application.GetCommand("WSGRI.DigitalFactory.DF2DControl.cmdPan");
                if (command != null) command.Execute();


                return;
            }

        }
    }
}
