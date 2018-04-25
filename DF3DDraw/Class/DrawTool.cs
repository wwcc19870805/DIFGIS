using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gvitech.CityMaker.FdeGeometry;
using DF3DControl.Base;
using Gvitech.CityMaker.RenderControl;
using Gvitech.CityMaker.FdeCore;
using Gvitech.CityMaker.Controls;
using DFCommon.Class;

namespace DF3DDraw
{
    public class DrawTool : IDrawTool
    {
        protected AxRenderControl _3DControl;
        protected IObjectManager _objectManager;
        protected IGeometryFactory _geoFactory;
        protected ICurveSymbol _curveSymbol;
        protected ISurfaceSymbol _surfaceSymbol;
        protected ISimplePointSymbol _simplePointSymbol;
        protected bool _isFinished;
        protected bool _isStarted;
        protected DrawType _geoType;
        protected IGeometry _geo;
        protected IFeatureLayerPickResult _selectFeatureLayerPickResult;
        protected IPoint _selectPoint;
        protected Guid _rootID;
        protected gviHeightStyle _heightStyle;
        public OnStartDraw _onStartDraw;
        public OnFinishedDraw _onFinishedDraw;
        public DrawType GeoType
        {
            get
            {
                return this._geoType;
            }
        }
        public bool IsFinished
        {
            get
            {
                return this._isFinished;
            }
        }

        public bool IsStarted
        {
            get
            {
                return this._isStarted;
            }
        }
        public event OnFinishedDraw OnFinishedDraw
        {
            add
            {
                this._onFinishedDraw += value;
            }
            remove
            {
                this._onFinishedDraw -= value;
            }
        }
        public event OnStartDraw OnStartDraw
        {
            add
            {
                this._onStartDraw += value;
            }
            remove
            {
                this._onStartDraw -= value;
            }
        }
        public DrawTool()
        {
            this._3DControl = DF3DApplication.Application.Current3DMapControl;
            if (this._3DControl != null)
            {
                if (this._3DControl.Terrain.IsRegistered && this._3DControl.Terrain.VisibleMask != gviViewportMask.gviViewNone) 
                    this._heightStyle = gviHeightStyle.gviHeightOnTerrain;
                else this._heightStyle = gviHeightStyle.gviHeightAbsolute;

                this._objectManager = this._3DControl.ObjectManager;
                this._geoFactory = new GeometryFactory();
                this._curveSymbol = new CurveSymbol();
                this._curveSymbol.Width = 0;
                this._curveSymbol.Color = Convert.ToUInt32(SystemInfo.Instance.LineColor, 16);
                this._surfaceSymbol = new SurfaceSymbol();
                this._surfaceSymbol.Color = Convert.ToUInt32(SystemInfo.Instance.FillColor, 16);
                this._surfaceSymbol.BoundarySymbol = this._curveSymbol;
                this._simplePointSymbol = new SimplePointSymbol();
                this._simplePointSymbol.FillColor = Convert.ToUInt32(SystemInfo.Instance.LineColor, 16);
                this._simplePointSymbol.OutlineColor = Convert.ToUInt32(SystemInfo.Instance.LineColor, 16);
                this._simplePointSymbol.Size = 10;
                this._simplePointSymbol.Style = gviSimplePointStyle.gviSimplePointCircle;
                this._rootID = this._3DControl.ProjectTree.RootID;
            }
        }

        public IFeatureLayerPickResult GetSelectFeatureLayerPickResult()
        {
            return this._selectFeatureLayerPickResult;
        }
        public IPoint GetSelectPoint()
        {
            return this._selectPoint;
        }

        public IGeometry GetGeo()
        {
            return this._geo;
        }

        public virtual void Close()
        {
        }

        public virtual void Start()
        {
            
        }

        public virtual void End()
        {
        }

        public static ITableLabel CreateTableLabel1(int rowcount)
        {
            AxRenderControl _3DControl = DF3DApplication.Application.Current3DMapControl;
            if (_3DControl != null)
            {
                ITableLabel tl = _3DControl.ObjectManager.CreateTableLabel(rowcount, 1, _3DControl.ProjectTree.RootID);
                tl.BorderColor = 0xcc068043;            // 表的边框颜色        
                tl.BorderWidth = 2;                     // 表的边框的宽度        
                tl.TableBackgroundColor = 0xccffffff;   // 表的背景色        
                tl.TitleBackgroundColor = 0xcc3366cc;   // 标题背景色  

                // 第一列文本样式
                TextAttribute contentTextAttribute = new TextAttribute();
                contentTextAttribute.TextColor = 0xff000000;
                contentTextAttribute.OutlineColor = 0xff000000;
                contentTextAttribute.Font = "仿宋";
                contentTextAttribute.TextSize = 11;
                contentTextAttribute.Bold = false;
                contentTextAttribute.MultilineJustification = gviMultilineJustification.gviMultilineLeft;
                tl.SetColumnTextAttribute(0, contentTextAttribute);

                // 标题文本样式
                TextAttribute capitalTextAttribute = new TextAttribute();
                capitalTextAttribute.TextColor = 0xffffffff;
                capitalTextAttribute.OutlineColor = 0xffffffff;
                capitalTextAttribute.Font = "宋体";
                capitalTextAttribute.TextSize = 11;
                capitalTextAttribute.MultilineJustification = gviMultilineJustification.gviMultilineCenter;
                capitalTextAttribute.Bold = false;
                tl.TitleTextAttribute = capitalTextAttribute;
                return tl;
            }
            return null;
        }
        public static ITableLabel CreateTableLabel2(int rowcount)
        {
            AxRenderControl _3DControl = DF3DApplication.Application.Current3DMapControl;
            if (_3DControl != null)
            {
                ITableLabel tableLabel = _3DControl.ObjectManager.CreateTableLabel(rowcount, 2, _3DControl.ProjectTree.RootID);
                tableLabel.BorderColor = 0xcc068043;            // 表的边框颜色        
                tableLabel.BorderWidth = 2;                     // 表的边框的宽度        
                tableLabel.TableBackgroundColor = 0xccffffff;   // 表的背景色        
                tableLabel.TitleBackgroundColor = 0xcc3366cc;   // 标题背景色  

                // 第一列文本样式
                TextAttribute headerTextAttribute = new TextAttribute();
                headerTextAttribute.TextColor = 0xff3366cc;
                headerTextAttribute.OutlineColor = 0xff3366cc;
                headerTextAttribute.Font = "仿宋";
                headerTextAttribute.TextSize = 11;
                headerTextAttribute.Bold = false;
                headerTextAttribute.MultilineJustification = gviMultilineJustification.gviMultilineLeft;
                tableLabel.SetColumnTextAttribute(0, headerTextAttribute);

                // 第二列文本样式
                TextAttribute contentTextAttribute = new TextAttribute();
                contentTextAttribute.TextColor = 0xff000000;
                contentTextAttribute.OutlineColor = 0xff000000;
                contentTextAttribute.Font = "仿宋";
                contentTextAttribute.TextSize = 11;
                contentTextAttribute.Bold = false;
                contentTextAttribute.MultilineJustification = gviMultilineJustification.gviMultilineLeft;
                tableLabel.SetColumnTextAttribute(1, contentTextAttribute);

                // 标题文本样式
                TextAttribute capitalTextAttribute = new TextAttribute();
                capitalTextAttribute.TextColor = 0xffffffff;
                capitalTextAttribute.OutlineColor = 0xffffffff;
                capitalTextAttribute.Font = "宋体";
                capitalTextAttribute.TextSize = 11;
                capitalTextAttribute.MultilineJustification = gviMultilineJustification.gviMultilineCenter;
                capitalTextAttribute.Bold = false;
                tableLabel.TitleTextAttribute = capitalTextAttribute;
                return tableLabel;
            }
            return null;
        }
    }
}
