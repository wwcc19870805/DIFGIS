/*-------------------------------------------------------------------
			// Copyright (C) 2005 中冶集团武汉勘察研究院有限公司
			// 版权所有。 
			//
			// 文件名：CalculateCurveParameter.cs
			// 文件功能描述：曲线元素计算
			//
			// 
			// 创建标识：YuanHY 20060510
            // 操作步骤：1、点击命令按纽；
			//           2、选择一个要素，弹出对话框;
			//           3、点击确定按钮实施操作。　　　　　　
			// 操作说明：ESC键 取消所有操作
			//           DEL键 删除选中的要素　　
			// 修改标识：　
			//           1、向框架状态栏传送提示信息　By YuanHY  20060615	
  		    // 修改标识：Modify by YuanHY20081112
            // 修改描述：增加了ESC键的操作　 
----------------------------------------------------------------------*/
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
    public class CalculateCurveParameter : AbstractMapCommand
    {
        private IDFApplication m_App;
        private IMapControl2 m_MapControl;
        private IMap m_FocusMap;
        private IActiveView m_pActiveView;
        private IMapView m_MapView = null;
        private ILayer m_CurrentLayer;

        private IPoint m_pPoint;
        private bool m_bIsUse;
        private INewEnvelopeFeedback m_pFeedbackEnve; //矩形框显示回馈

        private IArray m_OriginFeatureArray = new ArrayClass();

        private IStatusBarService m_pStatusBarService;//状态栏信息服务

        //private bool isEnabled = false;
        private string strCaption = "曲线元素计算";
        private string strCategory = "工具";

        private IEnvelope m_pEnvelope = new EnvelopeClass();


        private frmCalculateCurveParameter m_formCalculateCurveParameter;

        public CalculateCurveParameter()
        {
            //获得状态栏的服务
            m_pStatusBarService = (IStatusBarService)ServiceManager.Services.GetService(typeof(WSGRI.DigitalFactory.Services.UltraStatusBarService));

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

            CurrentTool.m_CurrentToolName = CurrentTool.CurrentToolName.CalculateCurveParameter;

            //CommonFunction.MapRefresh(m_pActiveView);

        }

        public override void UnExecute()
        {
            m_pStatusBarService.SetStateMessage("就绪");
        }

        public override void OnMouseDown(int button, int shift, int x, int y, double mapX, double mapY)
        {
            base.OnMouseDown(button, shift, x, y, mapX, mapY);

            m_CurrentLayer = m_App.CurrentEditLayer;

            m_pPoint = m_pActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(x, y);

            m_bIsUse = true;

            if (button == 2)//执行转化工作
            {
               DoCalculate();
            }
        }

        public override void OnDoubleClick(int button, int shift, int x, int y, double mapX, double mapY)
        {
            // TODO:  添加 ModifyAddVertex.OnDoubleClick 实现
            base.OnDoubleClick(button, shift, x, y, mapX, mapY);
            Reset();
        }

        public override void OnMouseMove(int button, int shift, int x, int y, double mapX, double mapY)
        {
            base.OnMouseMove(button, shift, x, y, mapX, mapY);

            m_MapControl.MousePointer = esriControlsMousePointer.esriPointerCrosshair;

            m_pStatusBarService.SetStateMessage("步骤:1.选择一段或多段铁路要素;2.右键/ENTER键/SPACEBAR/,结束选择，弹出铁路曲线元素计算对话框；3.指定用于拟合第一条切线、圆曲线、第二条切线的点;4.点击计算按钮。(ESC:取消)");

            if (!m_bIsUse) return;

            if (m_pFeedbackEnve == null)
            {
                m_pFeedbackEnve = new NewEnvelopeFeedbackClass();
                m_pFeedbackEnve.Display = m_pActiveView.ScreenDisplay;
                m_pFeedbackEnve.Start(m_pPoint);
            }

            m_pPoint = m_pActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(x, y);
            m_pFeedbackEnve.MoveTo(m_pPoint);

        }

        public override void OnMouseUp(int button, int shift, int x, int y, double mapX, double mapY)
        {
            base.OnMouseUp(button, shift, x, y, mapX, mapY);

            if (!m_bIsUse) return;

            IGeometry pEnv;

            if (m_pFeedbackEnve != null)
            {
                pEnv = m_pFeedbackEnve.Stop();
                m_FocusMap.SelectByShape(pEnv, null, false);
            }
            else
            {
                IEnvelope pRect;
                double dblConst;
                dblConst = CommonFunction.ConvertPixelsToMapUnits(m_pActiveView, 8);//8个象素大小
                pRect = CommonFunction.NewRect(m_pPoint, dblConst);
                m_FocusMap.SelectByShape(pRect, null, false);
            }

            IArray tempArray = CommonFunction.GetSelectFeatureSaveToArray(m_FocusMap);
            for (int i = 0; i < tempArray.Count; i++)
            {
                if (((IFeature)tempArray.get_Element(i)).Shape.GeometryType == esriGeometryType.esriGeometryPolyline ||
                    ((IFeature)tempArray.get_Element(i)).Shape.GeometryType == esriGeometryType.esriGeometryPolygon)
                {//只能选中线要素或面要素
                    m_OriginFeatureArray.Add((IFeature)tempArray.get_Element(i)); //将线要素添加到源数组中
                }
            }
            tempArray.RemoveAll();//请空临时数组

            CommonFunction.MadeFeatureArrayOnlyAloneOID(m_OriginFeatureArray);//使得源要素数组具有唯一性

            m_MapControl.ActiveView.FocusMap.ClearSelection(); //清空地图选择的要素

            m_pEnvelope = CommonFunction.GetMinEnvelopeOfTheFeatures(m_OriginFeatureArray);
            if (m_pEnvelope != null && !m_pEnvelope.IsEmpty) m_pEnvelope.Expand(1, 1, false);

            if (m_OriginFeatureArray.Count != 0)
            {
                m_MapControl.ActiveView.GraphicsContainer.DeleteAllElements();
                CommonFunction.ShowSelectionFeatureArray(m_MapControl, m_OriginFeatureArray);//高亮显示选择的要素

                for (int i = 0; i < m_OriginFeatureArray.Count; i++)
                {
                    IPointCollection pPointCollection = (IPointCollection)(m_OriginFeatureArray.get_Element(i) as IFeature ).Shape;

                    for (int j = 0; j < pPointCollection.PointCount; j++)
                    {
                        CommonFunction.DrawPointSMSSquareSymbol(m_MapControl, pPointCollection.get_Point(j));
                    }
                }
            }


            //选择复位
            m_pFeedbackEnve = null;
            m_bIsUse = false;

        }

        private void Reset()//取消所有操作
        {
            m_pFeedbackEnve = null;
            m_bIsUse = false;
            m_OriginFeatureArray.RemoveAll();

            m_pActiveView.GraphicsContainer.DeleteAllElements();
            m_pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, m_pEnvelope);//视图刷新

            m_pStatusBarService.SetStateMessage("就绪");

        }

        public override void Stop()
        {
            //this.Reset();
            base.Stop();
        }

        public override void OnKeyDown(int keyCode, int shift)
        {
            base.OnKeyDown(keyCode, shift);

            if (keyCode == 27)//ESC 键，取消所有操作
            {
                Reset();

                this.Stop();
                WSGRI.DigitalFactory.Commands.ICommand command = DFApplication.Application.GetCommand("WSGRI.DigitalFactory.DF2DControl.cmdPan");
                if (command != null) command.Execute();

                return;
            }

            if (keyCode == 13 || keyCode == 32)//按ENTER 键、SPACEBAR 键，执行转化工作
            {
                DoCalculate();//计算操作
            }

        }

        public void DoCalculate()//计算操作
        {
            if (m_OriginFeatureArray.Count == 0)
            {
                Reset();
                return;
            }
            System.Windows.Forms.Form m_mainForm = (System.Windows.Forms.Form)m_App.Workbench;

            frmCalculateCurveParameter.m_pFeatureArray = m_OriginFeatureArray;
            frmCalculateCurveParameter.m_pMapControl = this.m_MapControl;

            m_formCalculateCurveParameter = new frmCalculateCurveParameter();
            m_formCalculateCurveParameter.Owner = m_mainForm;

           // m_formCalculateCurveParameter.Close();

            m_formCalculateCurveParameter.Show();

            m_pFeedbackEnve = null;
            m_bIsUse = false;
            m_OriginFeatureArray.RemoveAll();


        }
    }
}
