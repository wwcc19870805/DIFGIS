using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.Resource;
using System;
using DF3DPipeCreateTool.Class;

namespace DF3DPipeCreateTool.ParamModeling
{
    public class DrawScanModel : DrawGeometry, IDrawScanModel, IDrawGeometry
    {
        // Fields
        protected uint[] _colors;           // 渲染颜色
        protected RenderType _renderType;   // 渲染类型
        protected string[] _tcNames;        // 贴图名

        // Methods
        public DrawScanModel()
        {
        }
        /// <summary>
        /// 绘制模型
        /// </summary>
        /// <param name="mp">模型定位点</param>
        /// <param name="fmodel">精模</param>
        /// <param name="smodel">简模</param>
        /// <returns></returns>
        public override bool Draw(out IModelPoint mp, out IModel fmodel, out IModel smodel)
        {
            return base.Draw(out mp, out fmodel, out smodel);
        }
        /// <summary>
        /// 设置渲染颜色
        /// </summary>
        /// <param name="colors">颜色值</param>
        public virtual void SetColorRender(uint[] colors)
        {
            this._renderType = RenderType.Color;
            this._colors = colors;
        }
        /// <summary>
        /// 设置渲染纹理
        /// </summary>
        /// <param name="colors">贴图名</param>
        public virtual void SetTextureRender(string[] tcNames)
        {
            this._renderType = RenderType.Texture;
            this._tcNames = tcNames;
        }

        // Properties
        public uint[] Colors
        {
            get
            {
                return this._colors;
            }
            set
            {
                this._colors = value;
            }
        }
        public RenderType RenderType
        {
            get
            {
                return this._renderType;
            }
            set
            {
                this._renderType = value;
            }
        }
        public string[] TextureNames
        {
            get
            {
                return this._tcNames;
            }
            set
            {
                this._tcNames = value;
            }
        }
    }
}
