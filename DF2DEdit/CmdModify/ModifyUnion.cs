/*-------------------------------------------------------------------------
			// Copyright (C) 2017 中冶集团武汉勘察研究院有限公司
			// 版权所有。 
			//
			// 文件名：ModifyUnion.cs
			// 文件功能描述：联合线\面(可能产生的新要素，放到目标图层中存储)
			//
			// 
			// 创建标识：LuoXuan 20171011
            // 操作步骤：1、点击命令按纽；
			//           2、选择多个线或面要素(可以选择多个图层的要素)；
			//			 3、点击右键\回车\空格键，实施联合操作。
			// 操作说明：
			//           1、ESC键 取消所有操作
			//           2、DEL键 删除选中的要素　　
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
using DF2DEdit.Class;
using DFWinForms.Service;

namespace DF2DEdit.CmdModify
{
	/// <summary>
	/// ModifyUnion 的摘要说明。
	/// </summary>

    public class ModifyUnion : AbstractMap2DCommand
	{
        private DF2DApplication m_App;
		private IMapControl2   m_MapControl;
		private IMap           m_FocusMap;
		private IActiveView    m_pActiveView;
		private ILayer         m_CurrentLayer;

		private IPoint m_pPoint;
		private bool   m_bIsSelect;//标识是否在选择要素		
		private INewEnvelopeFeedback  m_pFeedbackEnve; //矩形框显示回馈
		private IArray m_FeatureArray = new ArrayClass();//源要素数组
		private IEnvelope m_pEnvelope = new EnvelopeClass();

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

			CurrentTool.m_CurrentToolName = CurrentTool.CurrentToolName.modifyUnion;

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

			m_pPoint = m_pActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(x, y);

			m_bIsSelect = true;

			if (button == 2)//联合操作
			{	
				DoUnion();
			}

		}

		public override void OnDoubleClick(int button, int shift, int x, int y, double mapX, double mapY)
		{
			// TODO:  添加 ModifyAddVertex.OnDoubleClick 实现
			base.OnDoubleClick (button, shift, x, y, mapX, mapY);
			Reset();
		}

		public override void OnMouseMove(int button, int shift, int x, int y, double mapX, double mapY)
		{
			base.OnMouseMove (button, shift, x, y, mapX, mapY);

			m_MapControl.MousePointer = esriControlsMousePointer.esriPointerCrosshair ;

			m_pStatusBarService.SetStateMessage("步骤:1.选择多个要素(线或面);2.右键/回车/空格键,实施构成多部件(联合)操作。(ESC:取消/DEL:删除)");

			if(!m_bIsSelect ) return;

			m_pPoint = m_pActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(x, y);	
	
			if (m_pFeedbackEnve == null ) 
			{
				m_pFeedbackEnve = new NewEnvelopeFeedbackClass();
				m_pFeedbackEnve.Display = m_pActiveView.ScreenDisplay;
				m_pFeedbackEnve.Start(m_pPoint);
			}
			m_pFeedbackEnve.MoveTo(m_pPoint);		
						
		}
	
		public override void OnMouseUp(int button, int shift, int x, int y, double mapX, double mapY)
		{
			base.OnMouseUp (button, shift, x, y, mapX, mapY);

			if(!m_bIsSelect ) return;
			
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
				dblConst = Class.Common.ConvertPixelsToMapUnits(m_pActiveView,8);//8个象素大小
                pRect = Class.Common.NewRect(m_pPoint, dblConst);
				m_FocusMap.SelectByShape(pRect,null,false);
			}

            IArray tempArray = Class.Common.GetSelectedFeaturesSaveToArray(m_FocusMap, ((IFeatureLayer)m_App.CurrentEditLayer).FeatureClass.ShapeType);
			
			if(tempArray.Count>0)
			{
				if(((IFeature)tempArray.get_Element(0)).Shape.GeometryType == esriGeometryType.esriGeometryPoint) 
				{   //不能联合点要素
					Reset();
					return;
				}

				for (int i = 0; i<tempArray.Count; i++)
				{
					m_FeatureArray.Add((IFeature)tempArray.get_Element(i)); 
				}
				tempArray.RemoveAll();//清空临时数组             
        
				CommonFunction.MadeFeatureArrayOnlyAloneOID(m_FeatureArray);//使得源要素数组具有唯一性

				m_pEnvelope = CommonFunction.GetMinEnvelopeOfTheFeatures(m_FeatureArray);
				if(m_pEnvelope != null &&!m_pEnvelope.IsEmpty )  m_pEnvelope.Expand(1,1,false);

				if(m_FeatureArray.Count !=0)
				{
					m_MapControl.ActiveView.GraphicsContainer.DeleteAllElements();
					CommonFunction.ShowSelectionFeatureArray(m_MapControl,m_FeatureArray);//高亮显示选择的要素
				}
			}
			//选择复位
			m_pFeedbackEnve = null;				
			m_bIsSelect = false;
			m_FocusMap.ClearSelection();//清空地图选择的要素
			
		}
		
		private void Reset()//取消所有操作
		{
			m_bIsSelect = false;
			m_FeatureArray.RemoveAll();
			m_pFeedbackEnve =null;
			m_FocusMap.ClearSelection();//清空地图选择的要素
		
//			CommonFunction.m_SelectArray.RemoveAll();  // 增加  2007-09-28
//			CommonFunction.m_OriginArray.RemoveAll();  // 增加  2007-09-28
			m_pActiveView.GraphicsContainer.DeleteAllElements(); 
			m_pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, m_pEnvelope);//视图刷新
			m_pEnvelope  = null;

			m_pStatusBarService.SetStateMessage("就绪");

		}	

		public override void Stop()
		{
			//this.Reset();
			base.Stop();
		}

		public void DoUnion()//联合操作
		{
			if (m_FeatureArray.Count<2)
			{
				Reset();
				return;
			}

			ILayer pFeatureLayer;
			pFeatureLayer = m_App.CurrentEditLayer;
			IGeometry pOldGeometry;
			IFeature  pOldFeature;
			IGeometry pOtherGeo;
			
			IWorkspaceEdit pWorkspaceEdit;
			pWorkspaceEdit = (IWorkspaceEdit) CommonFunction.GetLayerWorkspace(pFeatureLayer);
			if (pWorkspaceEdit == null) return;
			if (!pWorkspaceEdit.IsBeingEdited()) return;
			pWorkspaceEdit.StartEditOperation();
	
			pOldFeature  = (IFeature)m_FeatureArray.get_Element(0);
			pOldGeometry = (IGeometry)pOldFeature.Shape;
			IArray pArrayPoint = new ArrayClass();//将坐标信息存储到点数组中
			pArrayPoint = CommonFunction.GeometryToArray(pOldFeature.ShapeCopy);

			for (int i=1; i<m_FeatureArray.Count; i++)//遍历每个选中的要素
			{				
				pOtherGeo    = (IGeometry)((IFeature)m_FeatureArray.get_Element(i)).Shape;				
				
				IArray pTempArrayPoint = new ArrayClass();//将坐标信息存储到点数组中
				pTempArrayPoint = CommonFunction.GeometryToArray(((IFeature)m_FeatureArray.get_Element(i)).ShapeCopy);
				for(int j=0;j<pTempArrayPoint.Count;j++)
				{
					pArrayPoint.Add(pTempArrayPoint.get_Element(j) as Point);
				}

				pOldGeometry = CommonFunction.UnionGeometry(pOldGeometry,pOtherGeo);						
			}		
	
			CommonFunction.AddFeature(m_MapControl,pOldGeometry,m_App.CurrentEditLayer,pOldFeature,pArrayPoint);

			//可以删除选中的要素了
			for (int i = 0; i < m_FeatureArray.Count; i++)//遍历每个选中的要素
			{				
				((IFeature)m_FeatureArray.get_Element(i)).Delete();								
			}

			m_App.Workbench.CommandBarManager.Tools["2dmap.DFEditorTool.Undo"].SharedProps.Enabled = true;

			pWorkspaceEdit.StopEditOperation();
			
			Reset();
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

			if (keyCode == 13 || keyCode == 32)//按ENTER、SPACEBAR键 开始绘制联合操作
			{       
				DoUnion();

				return;
			}

			if(keyCode == 46)   //DEL键,删除选中的要素
			{
				CommonFunction.DelFeaturesFromArray(m_MapControl,ref m_FeatureArray);
			    Reset();
				return;
			}
		}
		
	}
}
