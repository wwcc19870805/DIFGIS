/*-----------------------------------------------------------------------------
			// Copyright (C) 2005 中冶集团武汉勘察研究院有限公司
			// 版权所有。 
			//
			// 文件名：ModifySplit.cs
			// 文件功能描述：分割线\面(可能产生的新要素，放到目标图层中存储)
			//
			// 
			// 创建标识：YuanHY 20060107
            // 操作步骤：1、点击命令按纽；
			//           2、选择一个或多个线\面要素(只能选择当前层的线\面要素)；
			//			 3、点击右键\回车\空格键，开始绘制切割线；
			//           4、双击鼠标\回车\空格键，实施分割线\面。
            // 操作说明：
			//           1、ESC键 取消所有操作
			// 修改标识：
			//           1、增加捕捉效果　              YuanHY  20060217
			//           2、向框架状态栏传送提示信息　	YuanHY  20060615　
			//           3、使新产生的要素带有Z值、M值	YuanHY  20070725 　    
------------------------------------------------------------------------------*/
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
using WSGRI.DigitalFactory.DFSystem.DFConfig;
using WSGRI.DigitalFactory.DFEditorLib;
using WSGRI.DigitalFactory.Services;
using WSGRI.DigitalFactory.DFFunction;

using ICSharpCode.Core.Services;

namespace WSGRI.DigitalFactory.DFEditorTool
{
	/// <summary>
	/// ModifySplit 的摘要说明。
	/// </summary>
   
	public class ModifySplit:AbstractMapCommand
	{
		private IDFApplication m_App;
		private IMapControl2   m_MapControl;
		private IMap           m_FocusMap;
		private IActiveView    m_pActiveView;
		private ILayer         m_CurrentLayer;
		private IMapView       m_MapView = null;

		private IPoint m_pPoint;
		private bool   m_bIsSelect;            //标识是否在选择要素
		private bool   m_bBeginDrawLineFeed;   //标识开始绘制切割线
		private bool   m_bIsDrawLineFeed;      //标识正在绘制切割线
		
		private INewEnvelopeFeedback  m_pFeedbackEnve; //矩形框显示回馈
		private IDisplayFeedback      m_pFeedback;
		private INewLineFeedback      m_pLineFeed;
		private IArray m_OriginFeatureArray = new ArrayClass();//源要素数组

		private IStatusBarService m_pStatusBarService;//状态栏信息服务

		private bool	isEnabled   = false;
		private string	strCaption  = "分割线/面";
		private string	strCategory = "高级编辑"; 

		private IEnvelope m_pEnvelope = new EnvelopeClass();

		public ModifySplit()
		{
			//获得状态栏的服务
			//m_pStatusBarService = (IStatusBarService)ServiceManager.Services.GetService(typeof(WSGRI.DigitalFactory.Services.UltraStatusBarService));

		}

		#region 类的属性
		public override bool IsEnabled
		{
			get 
			{
				isEnabled = false;

				m_App = (IDFApplication)this.Hook;

				if (m_App == null)   return false;
				IMapView mapView     = null;
				mapView = (IMapView)m_App.Workbench.GetView(typeof(MapView));
				IFeatureLayer pFeatureLayer = (IFeatureLayer)m_App.CurrentEditLayer;
                if(m_App.CurrentEditLayer is IGeoFeatureLayer)
                {
					if (pFeatureLayer.FeatureClass.ShapeType == esriGeometryType.esriGeometryPolyline
						||pFeatureLayer.FeatureClass.ShapeType == esriGeometryType.esriGeometryPolygon)
						isEnabled = true;
				}
				return isEnabled;
			}
			set 
			{
				isEnabled = value;
			}
		}

		public override string Caption 
		{
			get
			{
				return strCaption;
			}
			set
			{
				strCaption = value ;
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
				strCategory = value ;
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

			m_MapControl   = m_App.CurrentMapControl;            
			m_FocusMap     = m_MapControl.ActiveView.FocusMap;
			m_pActiveView  = (IActiveView)this.m_FocusMap;
			m_CurrentLayer = m_App.CurrentEditLayer;
			m_pStatusBarService = m_App.StatusBarService;//获得状态服务

			CurrentTool.m_CurrentToolName = CurrentTool.CurrentToolName.modifySplit;

			//CommonFunction.MapRefresh(m_pActiveView);
           
		}
          
		public override void UnExecute()
		{
			// TODO:  添加 ModifyUnion.UnExecute 实现
			m_pStatusBarService.SetStateMessage("就绪");

		}
	
		public override void OnMouseDown(int button, int shift, int x, int y, double mapX, double mapY)
		{
			base.OnMouseDown (button, shift, x, y, mapX, mapY);
			
			m_CurrentLayer = m_App.CurrentEditLayer;

			m_bIsSelect = true;

			if (button == 2 && !m_bBeginDrawLineFeed)//右键单击，开始绘制分割线
			{	
				if (m_OriginFeatureArray.Count==0) return;
				
				m_bBeginDrawLineFeed = true;

				return;
			}

			if(button != 2 && m_bBeginDrawLineFeed)//绘制分割线(“剪刀”)工作
			{
				if (!m_bIsDrawLineFeed)//第1个点
				{
					m_bIsDrawLineFeed = true;
					m_pFeedback = new NewLineFeedbackClass(); 
					m_pLineFeed = (INewLineFeedback)m_pFeedback;
					m_pLineFeed.Start(m_pPoint);
					if( m_pFeedback != null)  m_pFeedback.Display = m_pActiveView.ScreenDisplay;
				}
				else//第2、3个点
				{
					m_pLineFeed = (INewLineFeedback)m_pFeedback;
					m_pLineFeed.AddPoint(m_pPoint);
				}
				
			}

			if (button == 2 && m_bBeginDrawLineFeed)//执行分割操作
			{	
				DoSplit();
			}

		}

		public override void OnMouseMove(int button, int shift, int x, int y, double mapX, double mapY)
		{
			base.OnMouseMove (button, shift, x, y, mapX, mapY);

			m_MapControl.MousePointer = esriControlsMousePointer.esriPointerCrosshair ;
				
			m_pStatusBarService.SetStateMessage("步骤:1.选择线/面要素(只能选择目标层要素);2.右键,结束选择;3.连续单击左键,确定分割线;4.右键,实施分割操作。(ESC:取消/DEL:删除)");

			if(!m_bIsSelect ) return;

			m_pPoint = m_pActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(x, y);
		
			if(!m_bBeginDrawLineFeed) //正在选择要被分割的线要素
			{
				if (m_pFeedbackEnve == null ) 
				{
					m_pFeedbackEnve = new NewEnvelopeFeedbackClass();
					m_pFeedbackEnve.Display = m_pActiveView.ScreenDisplay;
					m_pFeedbackEnve.Start(m_pPoint);
				}
				m_pFeedbackEnve.MoveTo(m_pPoint);	
			
				return;
			}
			else//正在绘制“剪刀”
			{
				//+++++++++++++开始捕捉+++++++++++++++++++++					    
				CommonFunction.Snap(m_MapControl,m_App.CurrentConfig.cfgSnapEnvironmentSet,null,m_pPoint);

				if(m_pFeedback != null) m_pFeedback.MoveTo(m_pPoint);				
			}		
			
		}
	
		public override void OnMouseUp(int button, int shift, int x, int y, double mapX, double mapY)
		{
			base.OnMouseUp (button, shift, x, y, mapX, mapY);

			if(!m_bIsSelect ) return;
			if(m_bBeginDrawLineFeed) return;
			
			IGeometry pEnv;
			m_FocusMap.ClearSelection();
			if (m_pFeedbackEnve != null)
			{
				pEnv = m_pFeedbackEnve.Stop();
				m_FocusMap.SelectByShape(pEnv, null,false);
			}
			else
			{
				IEnvelope pRect ;
				double dblConst ;
				dblConst =CommonFunction.ConvertPixelsToMapUnits(m_pActiveView,8);//8个象素大小
				pRect = CommonFunction.NewRect(m_pPoint,dblConst);
				m_FocusMap.SelectByShape(pRect,null,false);
			}

			if(!m_bBeginDrawLineFeed)//选择一个或多个要分割的线要素
			{	
				if (((IFeatureLayer)m_App.CurrentEditLayer).FeatureClass.ShapeType == esriGeometryType.esriGeometryPolyline
					|| ((IFeatureLayer)m_App.CurrentEditLayer).FeatureClass.ShapeType == esriGeometryType.esriGeometryPolygon) 
				{

					IArray tempArray = CommonFunction.GetSelectedFeaturesFromCurrentLayerSaveToArray(m_App.CurrentEditLayer);
					for (int i = 0; i<tempArray.Count; i++)
					{
						m_OriginFeatureArray.Add((IFeature)tempArray.get_Element(i)); 
					}
					tempArray.RemoveAll();//清空临时数组

					m_pEnvelope = CommonFunction.GetMinEnvelopeOfTheFeatures(m_OriginFeatureArray);
					if(m_pEnvelope != null &&!m_pEnvelope.IsEmpty )  m_pEnvelope.Expand(1,1,false);      
					CommonFunction.MadeFeatureArrayOnlyAloneOID(m_OriginFeatureArray);//使得源要素数组具有唯一性

					if(m_OriginFeatureArray.Count !=0) 	
					{
						m_MapControl.ActiveView.GraphicsContainer.DeleteAllElements();
						CommonFunction.ShowSelectionFeatureArray(m_MapControl,m_OriginFeatureArray);//高亮显示选择的要素
					}
				}

			}

			//选择复位
			m_pFeedbackEnve = null;				
			m_bIsSelect = false;
			m_FocusMap.ClearSelection();//清空地图选择的要素
			
		}
		
		public override void OnDoubleClick(int button, int shift, int x, int y, double mapX, double mapY)
		{
			base.OnDoubleClick (button, shift, x, y, mapX, mapY);

			DoSplit();         
		}
		
		public override void OnKeyDown(int keyCode, int shift)
		{
			base.OnKeyDown (keyCode, shift);
            
			if (keyCode == 27 )//ESC 键，取消所有操作
			{
				Reset();

                this.Stop();
                WSGRI.DigitalFactory.Commands.ICommand command = DFApplication.Application.GetCommand("WSGRI.DigitalFactory.DF2DControl.cmdPan");
                if (command != null) command.Execute();

				return;
			}
			
			if (keyCode == 46)   //DEL键,删除选中的要素
			{
				CommonFunction.DelFeaturesFromArray(m_MapControl,ref m_OriginFeatureArray);

				Reset();
			    
				return;
			}

			if (!m_bBeginDrawLineFeed)
			{
				if (keyCode == 13 || keyCode == 32)//按ENTER、SPACEBAR键开始绘制切割线
				{       
					m_bBeginDrawLineFeed = true;
				}

			}
			else//执行切割线\面操作
			{
				DoSplit(); 

				return;

			}

			

		}
	
		public void DoSplit()//分割线\面
		{
			switch(((IFeatureLayer)m_App.CurrentEditLayer).FeatureClass.ShapeType)
			{
				case esriGeometryType.esriGeometryPolyline:
					SplitPolylines();
					break;
				case esriGeometryType.esriGeometryPolygon:
					SplitPolygons();
					break;
			}

		}

		public void SplitPolylines()//分割线
		{
			m_pLineFeed = (INewLineFeedback)m_pFeedback;	
			IPolyline  pFeatureScissors = (IPolyline)m_pLineFeed.Stop();//结束绘制切割线
			if (pFeatureScissors.Length==0)
			{
				Reset();
				return;
			}

			ITopologicalOperator pTopologim_CalOperator = (ITopologicalOperator)pFeatureScissors;

			ILayer pFeatureLayer;
			pFeatureLayer = m_App.CurrentEditLayer;
			IGeometry pOldGeometry;
			IFeature  pOldFeature;
			
			IWorkspaceEdit pWorkspaceEdit; 

			pWorkspaceEdit = (IWorkspaceEdit) CommonFunction.GetLayerWorkspace(pFeatureLayer);
			if (pWorkspaceEdit == null) return;
			if (!pWorkspaceEdit.IsBeingEdited()) return;	
			pWorkspaceEdit.StartEditOperation();

			for (int i =0; i<m_OriginFeatureArray.Count; i++)//遍历每个选中的要素
			{
				pOldFeature=(IFeature)m_OriginFeatureArray.get_Element(i);
				pOldGeometry =(IGeometry)pOldFeature.Shape;

				IArray pArray =  new ArrayClass();//将地理要素的坐标信息添加到点数组中
				pArray = CommonFunction.GeometryToArray(pOldGeometry);

				//跳转到拓扑操作接口，求“剪刀”与选中要素的交点
				IGeometry pIntersectGeo = pTopologim_CalOperator.Intersect(pOldGeometry, esriGeometryDimension.esriGeometry0Dimension);
				if (pIntersectGeo == null) return ;//无交点，则返回

				ITopologicalOperator  pTopOp = (ITopologicalOperator) pIntersectGeo;
				pTopOp.Simplify();
				IPointCollection pPointCol = (IPointCollection)pIntersectGeo;//交点的集合

				//用相交的点集合打断该线
				IPointCollection pTmpPointCol = new MultipointClass();
				pTmpPointCol.AddPointCollection(pPointCol);//临时点集

				IPolycurve2 pPolyCurve;
				pPolyCurve = (IPolycurve2)pOldGeometry;//被剪切的线要素
				((ITopologicalOperator)pPolyCurve).Simplify();
								
				IGeometryCollection  pGeoCollection;
				IGeometryCollection  pTmpGeoCollection;    //保存每次打断产生的线段			

				pTmpGeoCollection =(IGeometryCollection)pPolyCurve;
				pGeoCollection    =(IGeometryCollection)pPolyCurve;	

				for( int j=0; j< pPointCol.PointCount; j++)//遍历每个交点
				{
					IPoint pSplitPoint = pPointCol.get_Point(j);
					
					int GeoCount = 0;
					int pGeoCollectionCount = pGeoCollection.GeometryCount;
					while(GeoCount < pGeoCollectionCount)//遍历每个几何形体
					{
						IPolycurve2 pTmpPolycurve2;
						pTmpPolycurve2 = CommonFunction.BuildPolyLineFromSegmentCollection((ISegmentCollection)pGeoCollection.get_Geometry(GeoCount));					

						bool bProject;   //是否投影
						bool bCreatePart;//是否创建新的附件
						bool bSplitted;  //分裂是否成功
						int lNewPart;
						int lNewSeg;
						bProject    = true;
						bCreatePart = true;

						((ITopologicalOperator)pTmpPolycurve2).Simplify();
					
						pTmpPolycurve2.SplitAtPoint(pSplitPoint,bProject,bCreatePart,out bSplitted,out lNewPart, out lNewSeg);
	
						if(bSplitted)//更新pGeoCollection
						{
							pGeoCollection.RemoveGeometries(GeoCount, 1);
							pTmpGeoCollection =(IGeometryCollection)pTmpPolycurve2;
							pGeoCollection.AddGeometryCollection(pTmpGeoCollection);
						}

						GeoCount++;
					}

				}

				IGeometryCollection pGeometryCol = pGeoCollection;//被打断后的线的集合
				for(int intCount = 0 ;intCount< pGeometryCol.GeometryCount;intCount++)
				{
					IPolycurve2  pPolyline = CommonFunction.BuildPolyLineFromSegmentCollection((ISegmentCollection)pGeometryCol.get_Geometry(intCount));
					CommonFunction.AddFeature(m_MapControl,(IGeometry)pPolyline,m_App.CurrentEditLayer, pOldFeature, pArray); 	
				}
				pOldFeature.Delete();
		
			}

			m_App.Workbench.CommandBarManager.Tools["2dmap.DFEditorTool.Undo"].SharedProps.Enabled = true;

			pWorkspaceEdit.StopEditOperation();
			
			Reset();

		}

		public void SplitPolygons()//分割面
		{
			m_pLineFeed = (INewLineFeedback)m_pFeedback;
	
			if(m_pLineFeed==null)
			{
				Reset();
				return;
			}

			IPolyline  pFeatureScissors = m_pLineFeed.Stop();//结束绘制切割线
			if (pFeatureScissors.Length==0)
			{
				Reset();
				return;
			}

			ILayer pFeatureLayer;
			pFeatureLayer = m_App.CurrentEditLayer;
			IGeometry pOldGeometry;
			IFeature  pOldFeature;
			
			IWorkspaceEdit pWorkspaceEdit; 

			pWorkspaceEdit = (IWorkspaceEdit) CommonFunction.GetLayerWorkspace(pFeatureLayer);
			if (pWorkspaceEdit == null) return;
			pWorkspaceEdit.StartEditOperation();
			
			for (int i =0; i<m_OriginFeatureArray.Count; i++)//遍历每个选中的要素
			{				
				IArray pArrGeo = new ArrayClass();
				pOldFeature=(IFeature)m_OriginFeatureArray.get_Element(i);
				pOldGeometry =(IGeometry)pOldFeature.Shape;

				if ((pOldGeometry == null) || (pFeatureScissors == null)) return ;
				if (pOldGeometry.GeometryType != esriGeometryType.esriGeometryPolygon) return ;

				ITopologicalOperator pTopologim_CalOperator = (ITopologicalOperator)pOldGeometry;
				IGeometry oRsGeo_1 = null, oRsGeo_2 = null;
				try
				{
					pTopologim_CalOperator.Simplify();
					pTopologim_CalOperator.Cut(pFeatureScissors, out oRsGeo_1, out oRsGeo_2);
				
					IGeometryCollection oGeoCol = (IGeometryCollection) oRsGeo_1;
					for (int j = 0; j < oGeoCol.GeometryCount; j++)
					{
						ISegmentCollection oNewPoly = new PolygonClass();
						oNewPoly.AddSegmentCollection((ISegmentCollection) oGeoCol.get_Geometry(j));
						pArrGeo.Add(oNewPoly);
					}
					oGeoCol = (IGeometryCollection) oRsGeo_2;
					for (int j = 0; j < oGeoCol.GeometryCount; j++)
					{
						ISegmentCollection oNewPoly = new PolygonClass();
						oNewPoly.AddSegmentCollection((ISegmentCollection) oGeoCol.get_Geometry(j));
						pArrGeo.Add(oNewPoly);
					}	

					for(int j=0;j<pArrGeo.Count;j++)
					{ 
						CommonFunction.AddFeature0(m_MapControl,(IGeometry)pArrGeo.get_Element(j), m_App.CurrentEditLayer, pOldFeature);
					}
					pOldFeature.Delete();
				}
				catch 
				{
					//MessageBox.Show(Ex.ToString());
				}
		
			}			
			m_App.Workbench.CommandBarManager.Tools["2dmap.DFEditorTool.Undo"].SharedProps.Enabled = true;

			pWorkspaceEdit.StopEditOperation();
			
			Reset();
		}

		private void Reset()//取消所有操作
		{
			m_bIsSelect = false;
			m_bBeginDrawLineFeed = false;
			m_OriginFeatureArray.RemoveAll();

			m_bIsDrawLineFeed    = false;
			m_pFeedback = null;
			m_pLineFeed = null;
			m_pFeedbackEnve = null;

//			CommonFunction.m_SelectArray.RemoveAll();  // 增加  2007-09-28
//			CommonFunction.m_OriginArray.RemoveAll();  // 增加  2007-09-28
			m_pActiveView.GraphicsContainer.DeleteAllElements(); 
			m_pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, m_pEnvelope);//视图刷新
			m_pEnvelope = null;

			m_pStatusBarService.SetStateMessage("就绪");

		}

		public override void Stop()
		{
			//this.Reset();
			base.Stop();
		}

	}
}


