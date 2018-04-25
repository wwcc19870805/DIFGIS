using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Text.RegularExpressions;

using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS;
using ESRI.ArcGIS.Controls;


namespace DF2DPipe.Class
{
    public class GlobalValue
    {
         private static SelectionEnvironmentClass m_System_SelectionEnvironment;
        private static SelectOption m_System_Selection_Option;

        public static SelectionEnvironmentClass System_Selection_Environment(IActiveView activeView)
        {
            if (m_System_Selection_Option == null)
            {
                m_System_Selection_Option = SelectOption.GetSelectOption();
            }
            if (m_System_SelectionEnvironment == null)
            {
                m_System_SelectionEnvironment = new SelectionEnvironmentClass();
            }

            double tol = PublicFunction.ConvertPixelsToMapUnits(activeView, m_System_Selection_Option.Tolerate);
            m_System_SelectionEnvironment.PointSearchDistance = tol;
            m_System_SelectionEnvironment.LinearSearchDistance = tol;
            m_System_SelectionEnvironment.AreaSearchDistance = tol;

            m_System_SelectionEnvironment.PointSelectionMethod = (esriSpatialRelEnum)m_System_Selection_Option.SelectRelection;
            m_System_SelectionEnvironment.LinearSelectionMethod = (esriSpatialRelEnum)m_System_Selection_Option.SelectRelection;
            m_System_SelectionEnvironment.AreaSelectionMethod = (esriSpatialRelEnum)m_System_Selection_Option.SelectRelection;

            m_System_SelectionEnvironment.ClearInvisibleLayers = m_System_Selection_Option.ClearInVisible;
            m_System_SelectionEnvironment.CombinationMethod = (esriSelectionResultEnum)m_System_Selection_Option.ResultMethod;
            m_System_SelectionEnvironment.DefaultColor = PublicFunction.GetSelectionColor(m_System_Selection_Option.DefaultColorRGB);
            m_System_SelectionEnvironment.SearchTolerance = (int)tol;
            m_System_SelectionEnvironment.ShowSelectionWarning = m_System_Selection_Option.ShowWarning;
            m_System_SelectionEnvironment.WarningThreshold = m_System_Selection_Option.WarningThreshold;
            return m_System_SelectionEnvironment;
        }

        /// <summary>
        /// 可以保存的选择参数
        /// </summary>
        /// <returns></returns>
        public static SelectOption System_Selection_Option()
        {
            if (m_System_Selection_Option == null)
            {
                m_System_Selection_Option = SelectOption.GetSelectOption();
            }
            return m_System_Selection_Option;
        }
    }
}
