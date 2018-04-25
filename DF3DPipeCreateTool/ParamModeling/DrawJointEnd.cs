using DF3DPipeCreateTool.Class;
using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.Resource;
using System;
namespace DF3DPipeCreateTool.ParamModeling
{
    public class DrawJointEnd : DrawJoint, IDrawJointEnd, IDrawJoint, IDrawScanModel, IDrawGeometry
    {
        // Fields
        private string _sourceModel;

        // Methods
        public DrawJointEnd()
        {
            base._modeltype = ModelType.JointEnd;
        }
        #region 断头绘制
        /// <summary>
        /// 原代码
        /// </summary>
        //public override bool Draw(out IModelPoint mp, out IModel fmodel, out IModel smodel)
        //{
        //    base.Draw(out mp, out fmodel, out smodel);
        //    try
        //    {
        //        IDrawGroup group;
        //        if ((base._vtx.Length == 1) && (base._sections.Length == 1))
        //        {
        //            object renderInfo = (base._renderType == RenderType.Texture) ? ((object)base._tcNames) : ((object)base._colors);
        //            int[] index = new int[3];
        //            index[1] = 1;
        //            index[2] = 2;
        //            if (base.NewEmptyModel(index, base._renderType, renderInfo, out fmodel))
        //            {
        //                int[] numArray2 = new int[1];
        //                if (base.NewEmptyModel(numArray2, base._renderType, renderInfo, out smodel))
        //                {
        //                    goto Label_0086;
        //                }
        //            }
        //        }
        //        return false;
        //    Label_0086:
        //        group = null;
        //        group = fmodel.GetGroup(0);
        //        IDrawPrimitive primitive = group.GetPrimitive(0);
        //        group.GetPrimitive(1);
        //        group.GetPrimitive(2);
        //        IDrawPrimitive primitive2 = smodel.GetGroup(0).GetPrimitive(0);
        //        base.DrawConSingle(base._vtx[0], base._sections[0], ref fmodel);
        //        primitive2.Material.CullMode = gviCullFaceMode.gviCullNone;
        //        primitive2.VertexArray = primitive.VertexArray;
        //        primitive2.NormalArray = primitive.NormalArray;
        //        primitive2.TexcoordArray = primitive.TexcoordArray;
        //        primitive2.IndexArray = primitive.IndexArray;
        //        return true;
        //    }
        //    catch (Exception exception)
        //    {
        //        SystemLog.Instance.Log(exception.StackTrace);
        //        return false;
        //    }
        //}
        /// <summary>
        /// 单面封口绘制 FX 2014.04.04
        /// </summary>
        public override bool Draw(out IModelPoint mp, out IModel fmodel, out IModel smodel)
        {
            base.Draw(out mp, out fmodel, out smodel);
            try
            {
                IDrawGroup group;
                if ((base._vtx.Length == 1) && (base._sections.Length == 1))
                {
                    object renderInfo = (base._renderType == RenderType.Texture) ? ((object)base._tcNames) : ((object)base._colors);
                    int[] index = new int[2];
                    index[1] = 1;
                    // 调整封口处颜色 与管壁颜色一致
                    uint[] numArray1 = renderInfo as uint[];
                    if (numArray1.Length > 1)
                    {
                        for (int i = 1; i < numArray1.Length; i++)
                        {
                            numArray1[i] = numArray1[0];
                        }
                    }
                    if (base.NewEmptyModel(index, base._renderType, renderInfo, out fmodel))
                    {
                        int[] numArray2 = new int[1];
                        if (base.NewEmptyModel(numArray2, base._renderType, renderInfo, out smodel))
                        {
                            goto Label_0086;
                        }
                    }
                }
                return false;
            Label_0086:
                group = null;
                group = fmodel.GetGroup(0);
                IDrawPrimitive primitive = group.GetPrimitive(0);
                IDrawPrimitive primitive2 = smodel.GetGroup(0).GetPrimitive(0);
                base.DrawConSingle1(base._vtx[0], base._sections[0], ref fmodel);
                primitive2.Material.CullMode = gviCullFaceMode.gviCullNone;
                primitive2.VertexArray = primitive.VertexArray;
                primitive2.NormalArray = primitive.NormalArray;
                primitive2.TexcoordArray = primitive.TexcoordArray;
                primitive2.IndexArray = primitive.IndexArray;
                return true;
            }
            catch (Exception exception)
            {
                return false;
            }
        }
        #endregion

        // Properties
        public string SourceModel
        {
            get
            {
                return this._sourceModel;
            }
            set
            {
                this._sourceModel = value;
            }
        }
    }
}
