/*---------------------------------------------------------------------
			// Copyright (C) 2017 中冶集团武汉勘察研究院有限公司
			// 版权所有。 
			//
			// 文件名：DrawLine.cs
			// 文件功能描述：绘制折线\面
			//
			// 
			// 创建标识：LuoXuan
            // 操作说明：U键回退
			//           A键输入绝对坐标
			//　　　　　 C键封闭结束
            //           E键\Enter键\Space键结束            
			//           ESC键取消所有操作
            // 修改描述：
-----------------------------------------------------------------------*/

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
	/// DrawLine 的摘要说明。
	/// </summary>
    public class DrawLine : AbstractMap2DCommand
	{
        private DF2DApplication m_App;      
		private IMapControl2   m_MapControl;
		private IMap           m_FocusMap;
		private ILayer         m_CurrentLayer;
		private IActiveView    m_pActiveView; 

		private IDisplayFeedback m_pFeedback;
		private IDisplayFeedback m_pLastFeedback;
		private INewLineFeedback m_pLineFeed;
		private INewLineFeedback m_pLastLineFeed;

		private bool          m_bInUse;
		public  static IPoint m_pPoint;
		public  static IPoint m_pAnchorPoint;
		private IPoint        m_pLastPoint;
		public static bool   m_bInputWindowCancel = true;//标识输入窗体是否被取消
		private double   m_dblTolerance;       //固定容限值
		private IArray   m_pUndoArray = new ArrayClass();	
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
            m_App.Workbench.SetStatusInfo("快捷键提示：U:回退/A:绝对坐标XY/C:封闭结束/Enter:结束/ESC:取消");//向状态栏传送提示信息
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
			// TODO:  添加 DrawLine.OnMouseDown 实现
			base.OnMouseDown (button, shift, x, y, mapX, mapY);

            m_CurrentLayer = Class.Common.CurEditLayer;

            if (Class.Common.PointIsOutMap(m_CurrentLayer, m_pAnchorPoint) == true)
            {
				DrawLineMouseDown(m_pAnchorPoint);	
			}
			else
			{
				XtraMessageBox.Show("超出地图范围");
			}
		}

		private void DrawLineMouseDown(IPoint pPoint )
		{   			     
			if(!m_bInUse)//如果命令没有使用
			{ 
				m_bInUse = true; 
  
				m_pUndoArray.Add(pPoint);

				m_pLastPoint = pPoint;

				Class.Common.DrawPointSMSSquareSymbol(m_MapControl,pPoint);

				m_pFeedback = new NewLineFeedbackClass(); 
				m_pLineFeed = (INewLineFeedback)m_pFeedback;
				m_pLineFeed.Start(pPoint);
				if( m_pFeedback != null)  
                    m_pFeedback.Display = m_pActiveView.ScreenDisplay;

				if (((IFeatureLayer)m_CurrentLayer).FeatureClass.ShapeType == esriGeometryType.esriGeometryPolygon)
				{　
					//若当前图层是面层，则显示回馈开始点到鼠标点的线段
					m_pLastFeedback = new NewLineFeedbackClass();
					m_pLastLineFeed = (INewLineFeedback)m_pLastFeedback;
					m_pLastLineFeed.Start(pPoint);
					if( m_pLastFeedback != null)  m_pLastFeedback.Display = m_pActiveView.ScreenDisplay;
				}

			}
			else//若果命令正使用中
			{
				m_pLineFeed.Stop();
				m_pLineFeed.Start(pPoint);
                 
				IPoint tempPoint = new PointClass();
				tempPoint.X = pPoint.X;
				tempPoint.Y = pPoint.Y;              
				m_pUndoArray.Add(tempPoint);
                
				m_pLastPoint = m_pAnchorPoint;

				Class.Common.DisplaypSegmentColToScreen(m_MapControl, ref m_pUndoArray);//可以刷新屏幕了
      
			}
		}
        
		public override void OnMouseMove(int button, int shift, int x, int y, double mapX, double mapY)
		{
			// TODO:  添加 DrawLine.OnMouseMove 实现
			base.OnMouseMove (button, shift, x, y, mapX, mapY);

            m_App.Workbench.SetStatusInfo("快捷键提示：U:回退/A:绝对坐标XY/C:封闭结束/Enter:结束/ESC:取消");//向状态栏传送提示信息

			m_pPoint = m_pActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(x, y);

            m_pAnchorPoint = m_pPoint;
            //+++++++++++++开始捕捉+++++++++++++++++++++			
            //bool flag = CommonFunction.Snap(m_MapControl, m_App.CurrentConfig.cfgSnapEnvironmentSet, (IGeometry)m_pLastPoint, m_pAnchorPoint);

            if (!m_bInUse) return;

			m_pFeedback.MoveTo(m_pAnchorPoint);
    
			if((m_pUndoArray.Count > 1) && ((((IFeatureLayer)m_CurrentLayer).FeatureClass.ShapeType == esriGeometryType.esriGeometryPolygon)))
			{
				if( m_pLastFeedback != null)  m_pLastFeedback.Display = m_pActiveView.ScreenDisplay;
				m_pLastFeedback.MoveTo(m_pAnchorPoint);
			}

		}
  
		public override void OnDoubleClick(int button, int shift, int x, int y, double mapX, double mapY)
		{
			// TODO:  添加 DrawLine.OnDoubleClick 实现
			base.OnDoubleClick (button, shift, x, y, mapX, mapY);
            
			EndDrawLine();
		}

		public void EndDrawLine()
		{       
			IGeometry pGeom = null;
			IPolyline pPolyline;
			IPolygon pPolygon;
			IPointCollection pPointCollection;
	
			//向数组添加结束点
			if (((IFeatureLayer)m_CurrentLayer).FeatureClass.ShapeType == esriGeometryType.esriGeometryPolyline)
			{
				m_pUndoArray.Add(m_pUndoArray.get_Element(0));		　
			}

			pPolyline =(IPolyline)Class.Common.MadeSegmentCollection(ref m_pUndoArray);
                         
			if(m_bInUse)
			{            
				switch (((IFeatureLayer)m_CurrentLayer).FeatureClass.ShapeType)
				{
					case  esriGeometryType.esriGeometryPolyline:
						pPointCollection =(IPointCollection)pPolyline;                 
						if(pPointCollection.PointCount < 2)
						{
							MessageBox.Show("线上必须有两个点!");
						}
						else
						{
							pGeom = (IGeometry)pPointCollection;                          
						}
						break;
					case  esriGeometryType.esriGeometryPolygon:                     
						pPolygon= Class.Common.PolylineToPolygon(pPolyline);
						pPointCollection =(IPointCollection) pPolygon;
						pGeom = (IGeometry)pPointCollection;
						if(pPointCollection.PointCount < 3)
						{
							MessageBox.Show("面上必须有三个点!");
						}
						else
						{
							pGeom = (IGeometry)pPointCollection;                            
						}
						break;
					default:
						break;
             
				}

				m_pEnvelope = pGeom.Envelope;
				if(m_pEnvelope != null &&!m_pEnvelope.IsEmpty )  m_pEnvelope.Expand(10,10,false);

				Class.Common.CreateFeature(pGeom, m_FocusMap, m_CurrentLayer);
                m_App.Workbench.UpdateMenu();   

				Reset();//复位  
			

			} 
		}
     
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
			if(m_pEnvelope != null &&!m_pEnvelope.IsEmpty )  m_pEnvelope.Expand(10,10,false);

			IPoint pPoint = new PointClass();
			pPoint = (IPoint)m_pUndoArray.get_Element(m_pUndoArray.Count-1);
			IEnvelope enve = new EnvelopeClass();
			enve =Class.Common.NewRect(pPoint,m_dblTolerance);

			IEnumElement  pEnumElement = m_MapControl.ActiveView.GraphicsContainer.LocateElementsByEnvelope(enve);
			if (pEnumElement != null)
			{
				pEnumElement.Reset();
				IElement pElement = pEnumElement.Next();

				while(pElement!=null)
				{
					m_MapControl.ActiveView.GraphicsContainer.DeleteElement(pElement);
					pElement = pEnumElement.Next();
				}
			}
				
			m_pUndoArray.Remove(m_pUndoArray.Count-1);//删除数组中最后一个点  
            
			//屏幕刷新
			m_pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, m_pEnvelope);//视图刷新
			m_pActiveView.ScreenDisplay.UpdateWindow();
       
			//开始作复位工作
			if (m_pUndoArray.Count!=0)
			{                
				Class.Common.DisplaypSegmentColToScreen(m_MapControl,ref m_pUndoArray);
   
				m_pLastPoint=(IPoint)m_pUndoArray.get_Element(m_pUndoArray.Count-1);               

				m_pFeedback = new NewLineFeedbackClass(); 
				m_pLineFeed =(NewLineFeedback)m_pFeedback;
				m_pLineFeed.Display = m_pActiveView.ScreenDisplay;
				if (m_pLineFeed !=null) m_pLineFeed.Stop();
				m_pLineFeed.Start(m_pLastPoint);
				m_pLineFeed.MoveTo(m_pPoint);       
			}
			else 
			{   
				Reset(); //复位
			}           
		}

		public override void OnBeforeScreenDraw(int hdc)
		{
			// TODO:  添加 DrawLine.OnBeforeScreenDraw 实现
			base.OnBeforeScreenDraw (hdc);
           
			if(m_pUndoArray.Count !=0)
			{
				IPoint pStartPoint = new PointClass();
				IPoint pEndPoint = new PointClass();
				pStartPoint = (IPoint)m_pUndoArray.get_Element(0);
				pEndPoint = (IPoint)m_pUndoArray.get_Element(m_pUndoArray.Count -1);

				if (m_pLineFeed !=null)      m_pLineFeed.MoveTo(pEndPoint);
				if (m_pLastLineFeed !=null)  m_pLastLineFeed.MoveTo(pStartPoint);
			}      
		}

		//复位
		public override void OnAfterScreenDraw(int hdc)
		{
			// TODO:  添加 DrawLine.OnAfterScreenDraw 实现
			base.OnAfterScreenDraw (hdc);
		}

		private void Reset()
		{
			m_pActiveView.FocusMap.ClearSelection();  
			m_pActiveView.GraphicsContainer.DeleteAllElements();//删除创建的地图元素

			m_pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, m_pEnvelope);//视图刷新

            m_App.Workbench.SetStatusInfo("就绪");

			m_bInUse = false;
			if(m_pLastPoint != null) m_pLastPoint.SetEmpty();;
			m_pUndoArray.RemoveAll();//清空回退数组 
			m_pLineFeed =null;
			m_pLastLineFeed=null;
			m_bInputWindowCancel = true;
			m_pEnvelope = null;

		}

		#region 键盘事件(定义的快捷键)
		public override void OnKeyDown(int keyCode, int shift)
		{
			// TODO:  添加 DrawLine.OnKeyDown 实现
			base.OnKeyDown (keyCode, shift);
         
			if (keyCode == 85 && m_bInUse)//按U键,回退
			{
				Undo();                
				return;
			}
            
			if (keyCode == 65)//按A键,输入绝对坐标
			{    				
				frmAbsXYZ.m_pPoint = m_pAnchorPoint;
				frmAbsXYZ formXYZ = new frmAbsXYZ();
				formXYZ.ShowDialog();

				if(m_bInputWindowCancel == false)//若用户没用取消输入
				{                    
					DrawLineMouseDown(m_pAnchorPoint);
				}

				return;
			}

			if (keyCode == 67 && m_pUndoArray.Count>=3)//按C键,封闭结束绘制
			{
				if(m_bInUse)
				{
					IPoint pStartPoint = new PointClass();
					pStartPoint=(IPoint)m_pUndoArray.get_Element(0);
					m_pUndoArray.Add(pStartPoint);

					EndDrawLine();
				}  

				return;
			}

			if ((keyCode == 69 || keyCode == 13 || keyCode == 32) && m_bInUse && m_pUndoArray.Count>=2)//按E键、ENTER 键、SPACEBAR 键结束绘制
			{
				EndDrawLine();
                
				return;
			}

			if (keyCode == 27)//ESC 键，取消所有操作
			{
				Reset();

                DF2DApplication.Application.Workbench.BarPerformClick("Pan");

				return;
			}

		}
		#endregion
	}
}
