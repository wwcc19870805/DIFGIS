using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DF2DControl.Command;
using ESRI.ArcGIS.Controls;
using DF2DControl.UserControl.View;
using DF2DControl.Base;
using DFWinForms.Service;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geodatabase;
using System.Windows.Forms;

namespace DF2DCreate.Command
{
    class CmdClear : AbstractMap2DCommand
    {
        private IMapControl2 m_pMapControl;
        private IMap2DView mapView;
        private DF2DApplication app;
        private IActiveView m_pActiveView;
        private IMap m_FocusMap;
        private IGraphicsContainer pGraphicsContainer;
        public override void Run(object sender, EventArgs e)
        {
            mapView = UCService.GetContent(typeof(Map2DView)) as Map2DView;
            if (mapView == null) return;
            bool bBind = mapView.Bind(this);
            if (!bBind) return;
            app = (DF2DApplication)this.Hook;
            if (app == null || app.Current2DMapControl == null) return;
            m_pMapControl = app.Current2DMapControl;
            m_pActiveView = m_pMapControl.ActiveView;
            m_FocusMap = m_pMapControl.ActiveView.FocusMap;

           /* IEnvelope pEnv = this.m_pMapControl.TrackRectangle();*/
//             IGeometry pGeo = pEnv as IGeometry;
//             this.m_pMapControl.Map.SelectByShape(pGeo, null, false);
//             this.m_pMapControl.Map.ClearSelection();
//             this.m_pMapControl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, m_pActiveView.Extent);

//             IRectangleElement rectangleElement = new RectangleElementClass();
//             IElement pelement = (IElement)rectangleElement;
//             pelement.Geometry = pEnv;
//            pGraphicsContainer = this.m_pMapControl.ActiveView as IGraphicsContainer;
//            pGraphicsContainer.AddElement(pelement, 0);
            this.m_pMapControl.ActiveView.GraphicsContainer.DeleteAllElements();
//            IGraphicsContainerSelect graphicsContainerSelect = (IGraphicsContainerSelect)this.m_pMapControl;
//             if (graphicsContainerSelect != null)
//             {
//                IEnumElement enumElement = graphicsContainerSelect.SelectedElements;
//                if (enumElement == null) return;
//                 
//                 enumElement.Reset();
// 
//                 IMap pMap = this.m_pMapControl.ActiveView.FocusMap;
// 
//                 IElement element = enumElement.Next();
//                 if (element != null)
//                 {
//                     if (element == app.Current2DMapControl.ActiveView.GraphicsContainer.FindFrame(pMap))
//                     {
//                         System.Windows.Forms.MessageBox.Show("地图数据框不可删除", "错误");
//                     }
//                     else
//                     {
//                         this.m_pMapControl.ActiveView.GraphicsContainer.DeleteElement(element);
//                     }
//                     element = enumElement.Next();
//                 }
                this.m_pMapControl.ActiveView.Refresh();

            }
    }
}

