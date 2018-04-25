/*-----------------------------------------------------------------------------------------
			// Copyright (C) 2017 中冶集团武汉勘察研究院有限公司
			// 版权所有。 
			//
			// 文件名：ModifyMove.cs
			// 文件功能描述：移动
			//
			// 
			// 创建标识：LuoXuan 20170822
            // 操作步骤：1、点击命令按纽；
			//           2、选择一个或多个点\线\面要素；
			//			 3、点击右键、ENTER 键、SPACEBAR 键，停止选择要素操作；
			//           4、点击左键，确定移动的第1个点；
			//           5、点击左键，确定移动的第2个点,完成移动操作。
            // 操作说明：
			//           1、ESC键 取消所有操作
-----------------------------------------------------------------------------------------*/
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
    /// ModifyMove 的摘要说明。
    /// </summary>
    public class ModifyMove : AbstractMap2DCommand
    {
        private DF2DApplication m_App;
        private IMapControl2 m_MapControl;
        private IMap m_FocusMap;
        private ILayer m_CurrentLayer;
        private IActiveView m_pActiveView;

        private static IPoint m_pPoint;
        private IPoint m_pPoint0 = new PointClass();
        private IPoint m_pPoint1 = new PointClass();
        private IPoint m_pPoint2 = new PointClass();

        private bool bBegineMove;//开始移动
        private bool bMoving;    //正在移动
        public static IPoint m_pAnchorPoint;
        private IPoint m_pLastPoint;
        private double m_dblTolerance;     //固定容限值

        private IArray m_OriginFeatureArray = new ArrayClass();

        private IElement m_pBasePointElement;

        private IEnvelope m_pEnvelope;//视图刷新的最小范围

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
            m_CurrentLayer = Class.Common.CurEditLayer;
            m_OriginFeatureArray.RemoveAll();
            m_dblTolerance = Class.Common.ConvertPixelsToMapUnits(m_MapControl.ActiveView, 4);
            bBegineMove = true;
            bMoving = false;

            IArray tempArray = Class.Common.GetSelectFeatureSaveToArray_2(m_FocusMap);
            for (int i = 0; i < tempArray.Count; i++)
            {
                m_OriginFeatureArray.Add((IFeature)tempArray.get_Element(i));
            }
            tempArray.RemoveAll();
            Class.Common.MadeFeatureArrayOnlyAloneOID(m_OriginFeatureArray);//保证数组元素的唯一性

            m_pEnvelope = Class.Common.GetMinEnvelopeOfTheFeatures(m_OriginFeatureArray);
            if (m_pEnvelope != null && !m_pEnvelope.IsEmpty) m_pEnvelope.Expand(1, 1, false);
        }

        public override void OnMouseDown(int button, int shift, int x, int y, double mapX, double mapY)
        {
            // TODO:  添加 ModifyMove.OnMouseDown 实现
            base.OnMouseDown(button, shift, x, y, mapX, mapY);

            if (button == 1 && bBegineMove)//确定移动的第1个点
            {
                bBegineMove = false;
                bMoving = true;

                m_pPoint0 = m_pAnchorPoint;
                m_pPoint1 = m_pAnchorPoint;
                m_pPoint2 = m_pAnchorPoint;
                m_pLastPoint = m_pAnchorPoint;

                m_pBasePointElement = Class.Common.DrawPointSMSXSymbol(m_MapControl, m_pAnchorPoint);

                return;
            }

            if (button == 1 && bMoving) //确定移动的第2个点,执行移动操作
            {
                m_pPoint2 = m_pAnchorPoint;

                MoveFeature();

                Reset();//复位

                return;
            }
        }

        public override void OnMouseMove(int button, int shift, int x, int y, double mapX, double mapY)
        {
            // TODO:  添加 ModifyMove.OnMouseMove 实现
            base.OnMouseMove(button, shift, x, y, mapX, mapY);

            m_MapControl.MousePointer = esriControlsMousePointer.esriPointerCrosshair;

            m_App.Workbench.SetStatusInfo("步骤:1.左键，确定移动的第1个基点;2.左键，确定移动的第2个基点,实施移动操作。(ESC:取消/DEL:删除)");

            m_pPoint = m_pActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(x, y);
            m_pAnchorPoint = m_pPoint;

            if (bBegineMove || bMoving)
            {
                //+++++++++++++开始捕捉+++++++++++++++++++++			
                //bool flag = Class.Common.Snap(m_MapControl, Class.SelectionEnv.cfgSnapEnvironmentSet, m_pPoint0, m_pPoint);
            }

            if (bMoving)//移动创建的地图元素，造成移动视觉
            {
                m_pPoint1 = m_pPoint2;
                m_pPoint2 = m_pAnchorPoint;

                if (m_pPoint1 != null && m_pPoint2 != null)
                {
                    if (!m_pPoint1.IsEmpty && !m_pPoint2.IsEmpty)
                    {
                        MoveElement();
                    }
                }
            }

        }

        public override void OnMouseUp(int button, int shift, int x, int y, double mapX, double mapY)
        {
            // TODO:  添加 ModifyMove.OnMouseUp 实现
            base.OnMouseUp(button, shift, x, y, mapX, mapY);

            if (bBegineMove) return;
            if (bMoving) return;
        }

        public override void OnKeyDown(int keyCode, int shift)
        {
            // TODO:  添加 ModifyMove.OnKeyDown 实现
            base.OnKeyDown(keyCode, shift);

            if (keyCode == 27)//ESC 键，取消所有操作
            {
                Reset();

                DF2DApplication.Application.Workbench.BarPerformClick("Pan");

                return;
            }
            if (keyCode == 46)   //DEL键,删除选中的要素
            {
                Class.Common.DelFeaturesFromArray(m_MapControl, ref m_OriginFeatureArray);

                Reset();

                return;
            }
        }

        private void Reset()
        {
            //m_bIsSelect = false;
            bBegineMove = false;
            bMoving = false;

            m_pPoint0.SetEmpty();
            m_pPoint1.SetEmpty();
            m_pPoint2.SetEmpty();
            m_pAnchorPoint.SetEmpty();

            if (m_pLastPoint != null) m_pLastPoint.SetEmpty();

            m_OriginFeatureArray.RemoveAll();

            //			CommonFunction.m_SelectArray.RemoveAll();  // 增加  2007-09-28
            //			CommonFunction.m_OriginArray.RemoveAll();  // 增加  2007-09-28
            m_pActiveView.GraphicsContainer.DeleteAllElements();//删除选择时创建的地图元素

            m_pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, m_pEnvelope);//视图刷新
            m_pEnvelope = null;

            m_App.Workbench.SetStatusInfo("就绪");

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
        }

        #region  //移动选定要素
        private void MoveFeature()
        {
            ITransform2D pTrans2d;
            IWorkspaceEdit pWorkspaceEdit;	//工作空间的编辑接口	
            
            pWorkspaceEdit = Class.Common.CurWspEdit;
            pWorkspaceEdit.StartEditOperation();

            m_pEnvelope = ((IFeature)m_OriginFeatureArray.get_Element(0)).Extent;

            for (int i = 0; i < m_OriginFeatureArray.Count; i++)
            {
                IFeature pFeature = (IFeature)m_OriginFeatureArray.get_Element(i);

                if (pFeature.FeatureType == esriFeatureType.esriFTAnnotation)
                {
                    IAnnotationFeature pAnnotationFeature = pFeature as IAnnotationFeature;
                    IElement element = new TextElementClass();
                    element = pAnnotationFeature.Annotation;

                    IPoint pPointOld = null;
                    if (element.Geometry.GeometryType == esriGeometryType.esriGeometryPolyline)
                    {
                        IPolyline pPolyline = element.Geometry as IPolyline;
                        pPointOld = pPolyline.FromPoint;
                    }
                    else if (element.Geometry.GeometryType == esriGeometryType.esriGeometryPoint)
                    {
                        pPointOld = element.Geometry as IPoint;
                    }
                    IPoint pPointNew = new PointClass();
                    pPointNew.PutCoords(pPointOld.X + (m_pPoint2.X - m_pPoint0.X), pPointOld.Y + (m_pPoint2.Y - m_pPoint0.Y));
                    element.Geometry = pPointNew;

                    try
                    {
                        pAnnotationFeature.Annotation = element;
                        ((IFeature)pAnnotationFeature).Store();
                    }
                    catch
                    {
                        System.Windows.Forms.MessageBox.Show("当前操作不在有效坐标范围内，操作失败");
                    }
                    m_pEnvelope.Union(pFeature.Shape.Envelope);

                }
                else
                {
                    pTrans2d = (ITransform2D)pFeature.Shape; //接口的跳转
                    pTrans2d.Move(m_pPoint2.X - m_pPoint0.X, m_pPoint2.Y - m_pPoint0.Y);

                    m_pEnvelope.Union(((IGeometry)pTrans2d).Envelope);

                    pFeature.Shape = (IGeometry)pTrans2d;
                    pFeature.Store();
                }
            }
            pWorkspaceEdit.StopEditOperation();
            m_App.Workbench.UpdateMenu();

            //m_pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, m_pEnvelope);//视图刷新


        }
        #endregion

        #region  //移动由选定地图要素创建的地图元素，造成移动视觉
        private void MoveElement()
        {
            ITransform2D pTrans2d;

            IGraphicsContainer pGraphicsContainer;
            pGraphicsContainer = m_MapControl.ActiveView.GraphicsContainer;

            pGraphicsContainer.Reset();
            IElement pElement = pGraphicsContainer.Next();

            if (pElement != null)
            {
                m_pEnvelope = pElement.Geometry.Envelope;
            }

            while (pElement != null)
            {
                if (!pElement.Equals((object)m_pBasePointElement))
                {

                    pTrans2d = (ITransform2D)pElement.Geometry; //接口的跳转
                    pTrans2d.Move(m_pPoint2.X - m_pPoint1.X, m_pPoint2.Y - m_pPoint1.Y);
                    pElement.Geometry = (IGeometry)pTrans2d;

                }

                m_pEnvelope.Union(pElement.Geometry.Envelope);

                pElement = pGraphicsContainer.Next();
            }

            m_pEnvelope.Expand(2, 2, false);

            m_pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, m_pEnvelope);//视图刷新
        }
        #endregion

    }
}
