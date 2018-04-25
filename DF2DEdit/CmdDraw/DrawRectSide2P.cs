/*------------------------------------------------------------------------
			// Copyright (C) 2017 中冶集团武汉勘察研究院有限公司
			// 版权所有。 
			//
			// 文件名：DrawRectSide2P.cs
			// 文件功能描述：给定:矩形一边的坐标 + 矩形宽度，绘制矩形线\面
			//
			// 
			// 创建标识：LuoXuan 20170927
            // 操作说明：U键回退
			//           A键输入绝对坐标
			//           E键\Enter键\Space键结束 
			//           ESC键取消所有操作       
            // 修改记录：
---------------------------------------------------------------------------*/

using System;
using System.Windows.Forms;

using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.esriSystem;

using DF2DControl.Base;
using DF2DControl.Command;
using DF2DControl.UserControl.View;
using DFWinForms.Service;
using DF2DEdit.Form;
using DevExpress.XtraEditors;

namespace DF2DEdit.CmdDraw
{
    /// <summary>
    /// DrawRectSide2P 的摘要说明。
    /// </summary>
    public class DrawRectSide2P : AbstractMap2DCommand
    {
        private DF2DApplication m_App;
        private IMapControl2 m_MapControl;
        private IMap m_FocusMap;
        private ILayer m_CurrentLayer;
        private IActiveView m_pActiveView;

        private IDisplayFeedback m_pFeedback;
        private INewLineFeedback m_pLineFeed;

        private bool m_bInUse;
        public  static IPoint m_pPoint;
        public  static IPoint m_pAnchorPoint;
        private IPoint m_pLastPoint;

        private int m_mouseDownCount;
        public  static bool m_bFixSideLength;//是否固定边长
        public  static double m_dblSideLength;
		public  static bool   m_bInputWindowCancel = true;//标识输入窗体是否被取消

		private double m_dblTolerance;     //固定容限值
		private ISegment m_pSegment = null;//平行尺方法修改锚点坐标时，捕捉到的边线的某个片断
		private bool m_bKeyCodeP;          //是否按P键，开启平行尺
		private IPoint   m_BeginConstructParallelPoint;//开始平行尺，鼠标右击的点
     
        private IArray m_pUndoArray = new ArrayClass();
        private IArray m_pSavePointArray = new ArrayClass();

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
            m_App.Workbench.SetStatusInfo("提示：依次确定一条边上两点、另一条边的宽度。(U:回退/A:绝对XY/B:边长/Enter:结束/ESC:取消)");//向状态栏传送提示信息
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
            // TODO:  添加 DrawRectSide2P.OnMouseDown 实现
            base.OnMouseDown (button, shift, x, y, mapX, mapY);

            //m_App.Workbench.SetStatusInfo("提示：依次确定一条边上两点、另一条边的宽度。(U:回退/A:绝对XY/Enter:结束/ESC:取消)");

            m_CurrentLayer = Class.Common.CurEditLayer;
			
			//检查点是否超出地图范围
			if(Class.Common.PointIsOutMap(m_CurrentLayer,m_pAnchorPoint) == true)
			{
				DrawRectSide2PMouseDown(m_pAnchorPoint);
			}
			else
			{
                XtraMessageBox.Show("超出地图范围");
			}			

        }
 
        private void DrawRectSide2PMouseDown(IPoint pPoint)
        {
			IPoint tempPoint = new PointClass();           

			m_mouseDownCount = m_mouseDownCount + 1;

            if (m_mouseDownCount < 3) //点击鼠标次数小于3时
            {	
				//绘制线  
                if (m_bInUse == false)
                {
                    m_pFeedback = new NewLineFeedbackClass();						
                    m_pLineFeed =(INewLineFeedback)m_pFeedback;
					m_pLineFeed.Start(pPoint);                                 

                    tempPoint.X = pPoint.X;
                    tempPoint.Y = pPoint.Y;

                    Class.Common.DrawPointSMSSquareSymbol(m_MapControl,pPoint);
                    m_pUndoArray.Add(tempPoint);//将第一个点保存到变量
                   
                    m_pLastPoint = pPoint;
                    m_bInUse = true;
                }
                else
                {
                    m_pLineFeed = (INewLineFeedback)m_pFeedback;
                    tempPoint.X = m_pAnchorPoint.X;
                    tempPoint.Y = m_pAnchorPoint.Y;
                    m_pLineFeed.AddPoint(tempPoint);

                    Class.Common.DrawPointSMSSquareSymbol(m_MapControl,tempPoint);
                    m_pUndoArray.Add(tempPoint);; //将第二个点保存到变量
                    
                }

                if ( m_pFeedback != null )  m_pFeedback.Display = m_pActiveView.ScreenDisplay;                
            }
            else if(m_mouseDownCount == 3) //点击鼠标次数等于3后,停止绘制
            {
				EndDrawRectSide2P();    
            } 
        }

		private void EndDrawRectSide2P()
		{
			IGeometry pGeom = null;
			IPolyline pPolyline;
			IPolygon  pPolygon;
			IPointCollection pPointCollection;
　　　　　　　　
			pPolyline =(IPolyline)Class.Common.MadeSegmentCollection(ref m_pSavePointArray);

			switch (((IFeatureLayer)m_CurrentLayer).FeatureClass.ShapeType)
			{
				case  esriGeometryType.esriGeometryPolyline:
					pPointCollection =(IPointCollection) pPolyline;
					pGeom = (IGeometry)pPointCollection;                       
					break;

				case esriGeometryType.esriGeometryPolygon:
					pPolygon  = Class.Common.PolylineToPolygon(pPolyline);
					pPointCollection =(IPointCollection) pPolygon;
					pGeom = (IGeometry)pPointCollection;                      
					break;

				default:
					break;

			}// end switch 

			m_pEnvelope = pGeom.Envelope;
			if(m_pEnvelope != null &&!m_pEnvelope.IsEmpty )  m_pEnvelope.Expand(10,10,false);

			Class.Common.CreateFeature(pGeom, m_FocusMap, m_CurrentLayer);
            m_App.Workbench.UpdateMenu();   

			Reset();
		}

        public override void OnMouseMove(int button, int shift, int x, int y, double mapX, double mapY)
        {
            // TODO:  添加 DrawRectSide2P.OnMouseMove 实现
            base.OnMouseMove (button, shift, x, y, mapX, mapY);
			
			m_pPoint = m_pActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(x, y);

			m_pAnchorPoint = m_pPoint;
			//+++++++++++++开始捕捉+++++++++++++++++++++			
            //CommonFunction.Snap(m_MapControl,m_App.CurrentConfig.cfgSnapEnvironmentSet,(IGeometry)m_pLastPoint,m_pAnchorPoint);

            if (m_bInUse == true)			
            {
                if(m_mouseDownCount == 1) //计数器=1时
                {
                    m_pPoint = m_pActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(x, y);

                    m_pAnchorPoint = m_pPoint;
                    
					m_pFeedback.MoveTo(m_pAnchorPoint);			
                }
                else if (m_mouseDownCount == 2) 
                {
                    m_pLineFeed.Stop();
				
                    //if(m_bFixDirection && m_bInputWindowCancel == false)  //固定m_pAnchorPoint使其在一个固定方向上
                    //{
                    //    m_pPoint = Class.Common.GetTwoPoint_FormPointMousePointFixDirection(m_pLastPoint,m_pPoint,m_dblFixDirection);
                    //    m_pAnchorPoint = m_pPoint;					
                    //}
					
					if ( !m_bFixSideLength )//获取矩形一边长
                    {
                        m_dblSideLength = Class.Common.GetRectangleOfSide_Length((IPoint)m_pUndoArray.get_Element(0), (IPoint)m_pUndoArray.get_Element(1), m_pAnchorPoint);
                    }
                    //else if (m_bInputWindowCancel == false)//按S键，用户输入边长，修正m_dblSideLength值
                    //{                            
                    //    bool bRight;//判断鼠标是否位于P1到P2方向的右边
                    //    bRight = CommonFunction.GetRectP0_Right((IPoint)m_pUndoArray.get_Element(0), (IPoint)m_pUndoArray.get_Element(1),m_pAnchorPoint);                            
                    //    if (bRight) m_dblSideLength = - m_dblSideLength;//对输入值反号                           
                    //}
													
                    //获取矩形另两点坐标
                    m_pSavePointArray.RemoveAll();
                    m_pSavePointArray = Class.Common.GetPointRectangle2((IPoint)m_pUndoArray.get_Element(0), (IPoint)m_pUndoArray.get_Element(1),m_dblSideLength); 
					  
                    Class.Common.DisplaypSegmentColToScreen(m_MapControl,ref m_pUndoArray);
                    //绘制矩形
                    m_pLineFeed.Start((IPoint)m_pUndoArray.get_Element(1));
                    m_pLineFeed.AddPoint((IPoint)m_pSavePointArray.get_Element(0));				 
                    m_pLineFeed.AddPoint((IPoint)m_pSavePointArray.get_Element(1));							
                    m_pLineFeed.AddPoint((IPoint)m_pUndoArray.get_Element(0));

                    m_pSavePointArray.Add((IPoint)m_pUndoArray.get_Element(0));
                    m_pSavePointArray.Add((IPoint)m_pUndoArray.get_Element(1));
                    m_pSavePointArray.Add((IPoint)m_pSavePointArray.get_Element(0));

                }//m_mouseDownCount == 2
                    
            }
                
        }

        //回退操作
        private void  Undo()
        {

			if(m_pUndoArray.Count >1)
			{
				m_pEnvelope = Class.Common.GetMinEnvelopeOfTheArray(m_pUndoArray);
			}
			else if(m_pUndoArray.Count ==1)
			{
				IPoint pTempPoint = new PointClass();
				pTempPoint.X = (m_pUndoArray.get_Element(0) as Point).X;
				pTempPoint.Y = (m_pUndoArray.get_Element(0) as Point).Y;

				m_pEnvelope.Width  = Math.Abs(m_pPoint.X - pTempPoint.X);
				m_pEnvelope.Height = Math.Abs(m_pPoint.Y - pTempPoint.Y);

				pTempPoint.X = (pTempPoint.X + m_pPoint.X)/2;
				pTempPoint.Y = (pTempPoint.Y + m_pPoint.Y)/2;

				m_pEnvelope.CenterAt(pTempPoint);				

			}
			if(m_pEnvelope != null &&!m_pEnvelope.IsEmpty )  m_pEnvelope.Expand(10,10,false);;

            m_pUndoArray.Remove(m_pUndoArray.Count-1);//删除数组中最后一个点  
            m_mouseDownCount--;
            
            //屏幕刷新
            m_pActiveView.PartialRefresh(esriViewDrawPhase.esriViewForeground,null,m_pEnvelope);
            m_pActiveView.ScreenDisplay.UpdateWindow();
       
            //开始作复位工作
            if (m_pUndoArray.Count!=0)
            {                
                Class.Common.DisplaypSegmentColToScreen(m_MapControl,ref m_pUndoArray);
   
                m_pLastPoint=(IPoint)m_pUndoArray.get_Element(m_pUndoArray.Count-1);               

				if (m_pLineFeed !=null) 
				{
					m_pLineFeed.Stop();
				}
				else
				{
					m_pFeedback = new NewLineFeedbackClass(); 
					m_pLineFeed =(NewLineFeedback)m_pFeedback;
					m_pLineFeed.Display = m_pActiveView.ScreenDisplay;  
				}
                m_pLineFeed.Start(m_pLastPoint);

				m_MapControl.ActiveView.GraphicsContainer.DeleteAllElements();
				Class.Common.DrawPointSMSSquareSymbol(m_MapControl,m_pLastPoint);      
				//m_MapControl.ActiveView.Refresh();  
				m_pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, m_pEnvelope);//视图刷新
            }
            else 
            {   //复位
                m_pFeedback.MoveTo(m_pAnchorPoint);
                Reset();
            }
           
        }

		private void Reset()
		{			
			m_pActiveView.GraphicsContainer.DeleteAllElements(); 
			m_pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, m_pEnvelope);//视图刷新

			m_pEnvelope = null;
			m_bInUse = false;
			m_pUndoArray.RemoveAll();//清空回退数组 
			m_pLineFeed = null;
			m_mouseDownCount = 0; 
			m_bFixSideLength=false;
			m_bInputWindowCancel = true;

            m_App.Workbench.SetStatusInfo("就绪");
		
		}
       
        public override void OnKeyDown(int keyCode, int shift)
        {
            // TODO:  添加 DrawRectSide2P.OnKeyDown 实现
            base.OnKeyDown (keyCode, shift);

			if (keyCode == 85 && m_mouseDownCount==1)//按U键,回退
			{
				Undo();

				return;
			}

			if (keyCode == 65 && m_mouseDownCount==1 )//按A键输入绝对坐标
			{     
				frmAbsXYZ.m_pPoint = m_pAnchorPoint;
				frmAbsXYZ formXYZ = new frmAbsXYZ();
				formXYZ.ShowDialog();
				if(m_bInputWindowCancel == false)//若用户没用取消输入
				{ 
					DrawRectSide2PMouseDown(m_pAnchorPoint);
				}

				return;
			}

			if ((keyCode == 69 || keyCode == 13 || keyCode == 32) && m_mouseDownCount==2)//按E键、ENTER 键、SPACEBAR 键结束绘制
			{
				EndDrawRectSide2P();
                
				return;
			}

			if (keyCode == 27 )//ESC 键，取消所有操作
			{
				Reset();

                DF2DApplication.Application.Workbench.BarPerformClick("Pan");

				return;
			}           

        }      
    }

}