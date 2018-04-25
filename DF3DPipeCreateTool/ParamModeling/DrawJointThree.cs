using DF3DPipeCreateTool.Class;
using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.Resource;
using System;
namespace DF3DPipeCreateTool.ParamModeling
{
    public class DrawJointThree : DrawJoint, IDrawJointThree, IDrawJoint, IDrawScanModel, IDrawGeometry
    {
        // Methods
        public DrawJointThree()
        {
            base._modeltype = ModelType.JointThree;
        }
        /// <summary>
        /// 绘制三通（原始代码）
        /// </summary>
        /// <param name="mp"></param>
        /// <param name="fmodel"></param>
        /// <param name="smodel"></param>
        /// <returns></returns>
        //public override bool Draw(out IModelPoint mp, out IModel fmodel, out IModel smodel)
        //{
        //    int[] index = null;
        //    base.Draw(out mp, out fmodel, out smodel);
        //    try
        //    {
        //        IDrawGroup group;
        //        if ((base._vtx.Length == 3) && (base._sections.Length == 3))
        //        {
        //            if (!PipeSection.SetConnectIndex(base._sections, base._vtx, out index))
        //            {
        //                return false;
        //            }
        //            if (!DrawGeometry.Compare(base._vtx[index[0]][0], base._vtx[index[1]][0], false))
        //            {
        //                return false;
        //            }
        //            object renderInfo = (base._renderType == RenderType.Texture) ? ((object)base._tcNames) : ((object)base._colors);
        //            int[] numArray2 = new int[3];
        //            numArray2[1] = 1;
        //            numArray2[2] = 2;
        //            if (base.NewEmptyModel(numArray2, base._renderType, renderInfo, out fmodel))
        //            {
        //                int[] numArray3 = new int[1];
        //                if (base.NewEmptyModel(numArray3, base._renderType, renderInfo, out smodel))
        //                {
        //                    goto Label_00CD;
        //                }
        //            }
        //        }
        //        return false;
        //    Label_00CD:
        //        group = null;
        //        group = fmodel.GetGroup(0);
        //        IDrawPrimitive primitive = group.GetPrimitive(0);
        //        group.GetPrimitive(1);
        //        group.GetPrimitive(2);
        //        IDrawPrimitive primitive2 = smodel.GetGroup(0).GetPrimitive(0);
        //        base.DrawConBetween(new Vector[][] { base._vtx[index[0]], base._vtx[index[1]] }, new IPipeSection[] { base._sections[index[0]], base._sections[index[1]] }, ref fmodel);
        //        base.DrawConSingle(base._vtx[index[2]], base._sections[index[2]], ref fmodel);
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
        /// 三通绘制/////////////-冯欣修改-20131101///////////////
        /// </summary>
        /// <param name="mp"></param>
        /// <param name="fmodel"></param>
        /// <param name="smodel"></param>
        /// <returns></returns>
        public override bool Draw(out IModelPoint mp, out IModel fmodel, out IModel smodel)
        {
            int[] index = null;
            base.Draw(out mp, out fmodel, out smodel);
            try
            {
                IDrawGroup group;
                if ((base._vtx.Length == 3) && (base._sections.Length == 3))
                {
                    if (!PipeSection.SetConnectIndex(base._sections, base._vtx, out index))
                    {
                        return false;
                    }
                    if (!DrawGeometry.Compare(base._vtx[index[0]][0], base._vtx[index[1]][0], false))
                    {
                        return false;
                    }
                    object renderInfo = (base._renderType == RenderType.Texture) ? ((object)base._tcNames) : ((object)base._colors);
                    int[] numArray2 = new int[1];
                    if (base.NewEmptyModel(numArray2, base._renderType, renderInfo, out fmodel))
                    {
                        int[] numArray3 = new int[1];
                        if (base.NewEmptyModel(numArray3, base._renderType, renderInfo, out smodel))
                        {
                            goto Label_00CD;
                        }
                    }
                }
                return false;
            Label_00CD:
                group = null;
                group = fmodel.GetGroup(0);
                IDrawPrimitive primitive = group.GetPrimitive(0);
                IDrawPrimitive primitive2 = smodel.GetGroup(0).GetPrimitive(0);
                base.DrawConBetween(new Vector[][] { base._vtx[index[0]], base._vtx[index[1]] }, new IPipeSection[] { base._sections[index[0]], base._sections[index[1]] }, ref fmodel);
                base.DrawConSingle(base._vtx[index[2]], base._sections[index[2]], ref fmodel);
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
    }
}
