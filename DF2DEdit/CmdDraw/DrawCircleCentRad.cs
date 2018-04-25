/*---------------------------------------------------------------------
			// Copyright (C) 2017 中冶集团武汉勘察研究院有限公司
			// 版权所有。 
			//
			// 文件名：DrawCircleCentRad.cs
			// 文件功能描述：给定:圆心坐标p + 半径，绘制圆\圆形区域
			//
			// 
			// 创建标识：LuoXuan20171010
            // 操作说明：
			//           A键输入绝对坐标
			//           B键输入圆心、半径
			//　　　　　 ESC键取消所有操作
			//           ENTER键、SPACEBAR键结束绘制
            //
-------------------------------------------------------------------------*/
using System;
using System.Windows.Forms;

using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geodatabase;

using DF2DControl.Base;
using DF2DControl.Command;
using DF2DControl.UserControl.View;
using DFWinForms.Service;
using DF2DEdit.Form;
using DevExpress.XtraEditors;

namespace DF2DEdit.CmdDraw
{
	/// <summary>
	/// DrawCircleCentRad 的摘要说明。
	/// </summary>
    public class DrawCircleCentRad : AbstractMap2DCommand
	{
        private DF2DApplication m_App;
        private IMapControl2 m_MapControl;
        private IMap           m_FocusMap;
        private ILayer         m_CurrentLayer;
        private IActiveView    m_pActiveView;

		private IDisplayFeedback   m_pFeedback;
		private INewCircleFeedback m_pCircleFeed;

        private bool   m_bInUse;
		public  static IPoint m_pPoint;
		public  static IPoint m_pAnchorPoint;
		private IPoint        m_pLastPoint;

		public  static IPoint m_pCenterPoint  = new PointClass();
        public  static bool   m_bFixRadius;
        public  static double m_dblRadius;
		public  static bool   m_bInputWindowCancel = true;//标识输入窗体是否被取消

		private double m_dblTolerance;     //固定容限值
		private IPoint   m_BeginConstructParallelPoint;//开始平行尺，鼠标右击的点
		private IEnvelope m_pEnvelope = new EnvelopeClass();

        public override void Run(object sender, System.EventArgs e)
        {
            Map2DCommandManager.Push(this);
            IMap2DView mapView = UCService.GetContent(typeof(Map2DView)) as Map2DView;
            if (mapView == null) return;
            bool bBind = mapView.Bind(this);
            if (!bBind) return;

            m_App = DF2DApplication.Application;
            if (m_App == null || m_App.Current2DMapControl == null) return;

            m_MapControl = m_App.Current2DMapControl;
            m_FocusMap = m_MapControl.ActiveView.FocusMap;
            m_pActiveView = (IActiveView)this.m_FocusMap;
            m_MapControl.MousePointer = esriControlsMousePointer.esriPointerCrosshair;
            m_dblTolerance = Class.Common.ConvertPixelsToMapUnits(m_MapControl.ActiveView, 4);

            Class.Common.MapRefresh(m_pActiveView);
            m_App.Workbench.SetStatusInfo("提示：依次指定1.圆心;2.圆周上的一点。(A:绝对XY/ESC:取消/ENTER:结束)");//向状态栏传送提示信息
        }

        public override void RestoreEnv()
        {
            IMap2DView mapView = UCService.GetContent(typeof(Map2DView)) as Map2DView;
            if (mapView == null) return;
            mapView.UnBind(this);
            DF2DApplication app = DF2DApplication.Application;
            if (app == null || app.Current2DMapControl == null) return;
            app.Current2DMapControl.MousePointer = esriControlsMousePointer.esriPointerDefault;
            Map2DCommandManager.Pop();

            Reset();
        }

        public override void OnMouseDown(int button, int shift, int x, int y, double mapX, double mapY)
        {
            // TODO:  添加 DrawCircleCentRad.OnMouseDown 实现
            base.OnMouseDown (button, shift, x, y, mapX, mapY);

            m_App.Workbench.SetStatusInfo("依次指定:1.圆心;2.圆周上的一点。(A:绝对XY/ESC:取消/ENTER:结束)");//向状态栏传送提示信息

            m_CurrentLayer = Class.Common.CurEditLayer;
			
			//检查点是否超出地图范围
			if(Class.Common.PointIsOutMap(m_CurrentLayer,m_pAnchorPoint) == true)
			{
				DrawCircleCentRadMouseDown(m_pAnchorPoint,shift);  
			}
			else
			{
				XtraMessageBox.Show("超出地图范围");
			}	
			 

        }

        //private void EndDrawCircleCentRadWihtShift()
        //{
        //    frmCentRad formCentRad = new frmCentRad();
        //    formCentRad.ShowDialog();

        //    if( m_bFixRadius )
        //    {
        //        IGeometry pGeom = null;
        //        IPolyline pPolyline;
        //        IPolygon  pPolygon;

        //   IPoint pPoint = new PointClass();
        //        pPoint.X = m_pCenterPoint.X + m_dblRadius;
        //        pPoint.Y = m_pCenterPoint.Y;
       
        //        pPolyline = CommonFunction.ArcToPolyline(pPoint, m_pCenterPoint, pPoint,esriArcOrientation.esriArcClockwise);
   
        //        switch (((IFeatureLayer)m_CurrentLayer).FeatureClass.ShapeType)
        //        {
        //            case  esriGeometryType.esriGeometryPolyline:  
        //                pGeom = pPolyline; 
        //                break;
        //            case esriGeometryType.esriGeometryPolygon:
        //                pPolygon  =  CommonFunction.PolylineToPolygon(pPolyline);
        //                pGeom = pPolygon;              　　                          
        //                break;
        //            default:
        //                break;
        //        }//end switch

        //        m_pEnvelope = pGeom.Envelope;
        //        if(m_pEnvelope != null &&!m_pEnvelope.IsEmpty )  m_pEnvelope.Expand(10,10,false);;
        //        CommonFunction.CreateFeature(m_App.Workbench,pGeom, m_FocusMap, m_CurrentLayer);

        //        m_App.Workbench.CommandBarManager.Tools["2dmap.DFEditorTool.Undo"].SharedProps.Enabled = true;    
                   
        //    }

        //    Reset();
        //}

		private void DrawCircleCentRadMouseDown(IPoint pPoint,int shift)
		{			
			Class.Common.DrawPointSMSSquareSymbol(m_MapControl,pPoint);	

			if(!m_bInUse)//如果命令没有使用
			{ 
				m_bInUse = true;
				m_pCenterPoint = pPoint;

				m_pFeedback = new NewCircleFeedbackClass();
				m_pCircleFeed = (NewCircleFeedbackClass)m_pFeedback;
				m_pCircleFeed.Display = m_pActiveView.ScreenDisplay;             
				m_pCircleFeed.Start(m_pCenterPoint);            
			}
			else //如果命令已经使用使用
			{
				IGeometry pGeom = null;
				IPolyline pPolyline;
				IPolygon  pPolygon;
				ICircularArc pCircularArc = new CircularArcClass();

                //if (shift == 1)//若果按住shift健弹出对话框，让用户修改圆周上的坐标值
                //{
                //     EndDrawCircleCentRadWihtShift();
                //}
                //else
                //{
                    m_pFeedback.MoveTo(pPoint);
					pCircularArc = m_pCircleFeed.Stop();
					m_dblRadius= pCircularArc.Radius;

					switch (((IFeatureLayer)m_CurrentLayer).FeatureClass.ShapeType)
					{
						case  esriGeometryType.esriGeometryPolyline:  
							pPolyline = Class.Common.ArcToPolyline(pCircularArc.FromPoint, pCircularArc.CenterPoint, pCircularArc.FromPoint,esriArcOrientation.esriArcClockwise);
							pGeom = pPolyline; 
							break;
						case esriGeometryType.esriGeometryPolygon:
							pPolyline = Class.Common.ArcToPolyline(pCircularArc.FromPoint, pCircularArc.CenterPoint, pCircularArc.FromPoint,esriArcOrientation.esriArcClockwise);
							pPolygon  =  Class.Common.PolylineToPolygon(pPolyline);
							pGeom = pPolygon;              　　                          
							break;
						default:
							break;
					}//end switch

					m_pEnvelope = pGeom.Envelope;
					if(m_pEnvelope != null &&!m_pEnvelope.IsEmpty )  m_pEnvelope.Expand(10,10,false);

					Class.Common.CreateFeature(pGeom, m_FocusMap, m_CurrentLayer);
                    m_App.Workbench.UpdateMenu();   

					Reset();

                //}
			}

			m_pLastPoint = pPoint;
		}

   
        public override void OnMouseMove(int button, int shift, int x, int y, double mapX, double mapY)
        {
            // TODO:  添加 DrawCircleCentRad.OnMouseMove 实现
            base.OnMouseMove (button, shift, x, y, mapX, mapY);

			
			m_pPoint = m_pActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(x, y);
			
			m_pAnchorPoint = m_pPoint;

			//+++++++++++++开始捕捉+++++++++++++++++++++	
            //if(m_pCenterPoint.IsEmpty)
            //{
            //    bool flag = CommonFunction.Snap(m_MapControl,m_App.CurrentConfig.cfgSnapEnvironmentSet,null,m_pAnchorPoint);
            //}
            //else
            //{
            //    bool flag = CommonFunction.Snap(m_MapControl,m_App.CurrentConfig.cfgSnapEnvironmentSet,m_pCenterPoint,m_pAnchorPoint);
            //}
        }

        public override void OnBeforeScreenDraw(int hdc)
        {
            // TODO:  添加 DrawCircleCentRad.OnBeforeScreenDraw 实现
            base.OnBeforeScreenDraw (hdc);
           
            if (m_pFeedback !=null)  
            {
                m_pFeedback.MoveTo(m_pAnchorPoint);              
            }    
        }

        public override void OnKeyDown(int keyCode, int shift)
        {
            // TODO:  添加 DrawCircleCentRad.OnKeyDown 实现
            base.OnKeyDown (keyCode, shift);
            
			if (keyCode == 65)//按A键,输入绝对坐标
			{    				
				frmAbsXYZ.m_pPoint = m_pAnchorPoint;
				frmAbsXYZ formXYZ = new frmAbsXYZ();
				formXYZ.ShowDialog();

				if(m_bInputWindowCancel == false)//若用户没用取消输入
				{                    
					DrawCircleCentRadMouseDown(m_pAnchorPoint,0);
				}


				return;
			}


			if ((keyCode == 13 || keyCode == 32) && m_bInUse)//按ENTER 键、SPACEBAR 键
			{   
				DrawCircleCentRadMouseDown(m_pAnchorPoint,shift); 
 
				return;
			
			}

			if (keyCode == 27 )//ESC 键，取消所有操作
			{
				Reset();

                DF2DApplication.Application.Workbench.BarPerformClick("Pan");

                return;
			}			
        }

		private void Reset()
		{
			m_bFixRadius = false;
			m_bInUse = false;
			m_bInputWindowCancel = true;
			m_pCircleFeed = null;
			if(m_pCenterPoint != null) m_pCenterPoint.SetEmpty();
			m_pFeedback = null;
			if(m_pLastPoint != null) m_pLastPoint.SetEmpty();

			m_pActiveView.GraphicsContainer.DeleteAllElements();
			m_pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, m_pEnvelope);//视图刷新

            m_App.Workbench.SetStatusInfo("就绪");

		}
    }
}
