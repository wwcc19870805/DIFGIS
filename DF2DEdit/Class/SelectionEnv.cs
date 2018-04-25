/*----------------------------------------------------------------
            // Copyright (C) 2017 中冶集团武汉勘察研究院有限公司
            // 版权所有。 
            //
            // 文件名：SelectionEnv.cs
            // 文件功能描述：选择环境
            //               
            // 
            // 创建标识：LuoXuan
            //
            // 修改描述：

----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;

namespace DF2DEdit.Class
{
    public class SelectionEnv
    {
        public SelectionEnv()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        private static int m_SelectRelation = 1;
        private static bool m_ClearInVisible = true;
        private static int m_Tolerate = 1;
        private static int m_DefaultColorRGB = -12517377;
        private static int m_ResultMethod = 0;
        private static SelectionEnvironmentClass m_System_SelectionEnvironment;

        public static int SelectRelation
        {
            get { return m_SelectRelation; }
            set { m_SelectRelation = value; }
        }
        public static bool ClearInVisible
        {
            get { return m_ClearInVisible; }
            set { m_ClearInVisible = value; }
        }
        public static int Tolerate
        {
            get { return m_Tolerate; }
            set { m_Tolerate = value; }
        }
        public static int DefaultColorRGB
        {
            get { return m_DefaultColorRGB; }
            set { m_DefaultColorRGB = value; }
        }
        public static int ResultMethod
        {
            get { return m_ResultMethod; }
            set { m_ResultMethod = value; }
        }

        public static SelectionEnvironmentClass System_Selection_Environment(IActiveView activeView)
        {
            if (m_System_SelectionEnvironment == null)
            {
                m_System_SelectionEnvironment = new SelectionEnvironmentClass();
            }

            double tol = Class.Common.ConvertPixelsToMapUnits(activeView, m_Tolerate);
            m_System_SelectionEnvironment.PointSearchDistance = m_Tolerate;
            m_System_SelectionEnvironment.LinearSearchDistance = m_Tolerate;
            m_System_SelectionEnvironment.AreaSearchDistance = m_Tolerate;

            m_System_SelectionEnvironment.PointSelectionMethod = (esriSpatialRelEnum)m_SelectRelation;
            m_System_SelectionEnvironment.LinearSelectionMethod = (esriSpatialRelEnum)m_SelectRelation;
            m_System_SelectionEnvironment.AreaSelectionMethod = (esriSpatialRelEnum)m_SelectRelation;

            m_System_SelectionEnvironment.ClearInvisibleLayers = m_ClearInVisible;
            m_System_SelectionEnvironment.CombinationMethod = (esriSelectionResultEnum)m_ResultMethod;
            m_System_SelectionEnvironment.DefaultColor = Class.Common.GetSelectionColor(m_DefaultColorRGB);
            m_System_SelectionEnvironment.SearchTolerance = m_Tolerate;
            return m_System_SelectionEnvironment;
        }

    }
}
