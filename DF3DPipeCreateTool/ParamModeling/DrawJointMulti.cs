using DF3DPipeCreateTool.Class;
using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.Resource;
using System;
namespace DF3DPipeCreateTool.ParamModeling
{
    public class DrawJointMulti : DrawJoint, IDrawJointMulti, IDrawJoint, IDrawScanModel, IDrawGeometry
    {
        // Methods
        public DrawJointMulti()
        {
            base._modeltype = ModelType.JointMulti;
        }

        #region 双壁绘制－－原代码
        //public override bool Draw(out IModelPoint mp, out IModel fmodel, out IModel smodel)
        //{
        //    int[] numArray = null;
        //    base.Draw(out mp, out fmodel, out smodel);
        //    try
        //    {
        //        IDrawGroup group;
        //        if ((base._vtx.Length == 4) && (base._sections.Length == 4))
        //        {
        //            numArray = new int[] { 0, 1, 2, 3 };
        //            if (!DrawGeometry.Compare(base._vtx[numArray[0]][0], base._vtx[numArray[1]][0], false))
        //            {
        //                return false;
        //            }
        //            object renderInfo = (base._renderType == RenderType.Texture) ? ((object)base._tcNames) : ((object)base._colors);
        //            int[] index = new int[3];
        //            index[1] = 1;
        //            index[2] = 2;
        //            if (base.NewEmptyModel(index, base._renderType, renderInfo, out fmodel))
        //            {
        //                int[] numArray3 = new int[1];
        //                if (base.NewEmptyModel(numArray3, base._renderType, renderInfo, out smodel))
        //                {
        //                    goto Label_00C2;
        //                }
        //            }
        //        }
        //        return false;
        //    Label_00C2:
        //        group = null;
        //        group = fmodel.GetGroup(0);
        //        IDrawPrimitive primitive = group.GetPrimitive(0);
        //        group.GetPrimitive(1);
        //        group.GetPrimitive(2);
        //        IDrawPrimitive primitive2 = smodel.GetGroup(0).GetPrimitive(0);
        //        base.DrawConBetween(new Vector[][] { base._vtx[numArray[0]], base._vtx[numArray[1]] }, new IPipeSection[] { base._sections[numArray[0]], base._sections[numArray[1]] }, ref fmodel);
        //        base.DrawConSingle(base._vtx[numArray[2]], base._sections[numArray[2]], ref fmodel);
        //        base.DrawConSingle(base._vtx[numArray[3]], base._sections[numArray[3]], ref fmodel);
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
        #endregion

        #region 单壁绘制－－冯欣修改20131101
        public override bool Draw(out IModelPoint mp, out IModel fmodel, out IModel smodel)
        {
            int[] numArray = null;
            base.Draw(out mp, out fmodel, out smodel);
            try
            {
                IDrawGroup group;
                if ((base._vtx.Length == 4) && (base._sections.Length == 4))
                {
                    numArray = new int[] { 0, 1, 2, 3 };
                    if (!DrawGeometry.Compare(base._vtx[numArray[0]][0], base._vtx[numArray[1]][0], false))
                    {
                        return false;
                    }
                    object renderInfo = (base._renderType == RenderType.Texture) ? ((object)base._tcNames) : ((object)base._colors);
                    int[] index = new int[1];
                    if (base.NewEmptyModel(index, base._renderType, renderInfo, out fmodel))
                    {
                        int[] numArray3 = new int[1];
                        if (base.NewEmptyModel(numArray3, base._renderType, renderInfo, out smodel))
                        {
                            goto Label_00C2;
                        }
                    }
                }
                return false;
            Label_00C2:
                group = null;
                group = fmodel.GetGroup(0);
                IDrawPrimitive primitive = group.GetPrimitive(0);
                IDrawPrimitive primitive2 = smodel.GetGroup(0).GetPrimitive(0);
                base.DrawConBetween(new Vector[][] { base._vtx[numArray[0]], base._vtx[numArray[1]] }, new IPipeSection[] { base._sections[numArray[0]], base._sections[numArray[1]] }, ref fmodel);
                base.DrawConSingle(base._vtx[numArray[2]], base._sections[numArray[2]], ref fmodel);
                base.DrawConSingle(base._vtx[numArray[3]], base._sections[numArray[3]], ref fmodel);
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
    }
}
