using DF3DPipeCreateTool.Class;
using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.Resource;
using System;
namespace DF3DPipeCreateTool.ParamModeling
{
    public class DrawConnBlend : DrawJoint, IDrawConnBlend, IDrawJoint, IDrawScanModel, IDrawGeometry
    {
        // Fields
        private IModel _bfModel;
        private IModel _bsModel;

        // Methods
        public DrawConnBlend()
        {
            base._modeltype = ModelType.ConnBlend;
        }

        public override bool Draw(out IModelPoint mp, out IModel fmodel, out IModel smodel)
        {
            base.Draw(out mp, out fmodel, out smodel);
            try
            {
                if (base._vtx == null)
                {
                    return false;
                }
                if ((base._vtx.Length != 1) && (base._vtx.Length != 2))
                {
                    return false;
                }
                object renderInfo = (base._renderType == RenderType.Texture) ? ((object)base._tcNames) : ((object)base._colors);
                //int[] index = new int[3];
                //index[1] = 1;
                //index[2] = 2;
                int[] index = new int[2];
                index[1] = 1;
                if (!base.NewEmptyModel(index, base._renderType, renderInfo, out fmodel))
                {
                    return false;
                }
                IDrawGroup group = null;
                group = fmodel.GetGroup(0);
                IDrawPrimitive primitive = group.GetPrimitive(0);
                group.GetPrimitive(1);
                group.GetPrimitive(2);
                if (base._vtx.Length == 1)
                {
                    // 生成类似断头的管线20161111
                    IPipeSection[] dSections = new IPipeSection[] { base._sections[0], DrawGeometry.DefaultSection(base._sections[0].SecShape) };
                    base.DrawConSingle1(base._vtx[0], base._sections[0], ref fmodel);

                    // 原代码
                    //Vector[][] dVtxs = new Vector[][] { base._vtx[0], new Vector[] { base._vtx[0][0], new Vector(base._vtx[0][0].X, base._vtx[0][0].Y, 0.0) } };
                    //IPipeSection[] dSections = new IPipeSection[] { base._sections[0], DrawGeometry.DefaultSection(base._sections[0].SecShape) };
                    //base.DrawConBetween(dVtxs, dSections, ref fmodel);
                }
                else
                {
                    base.DrawConBetween(new Vector[][] { base._vtx[0], base._vtx[1] }, new IPipeSection[] { base._sections[0], base._sections[1] }, ref fmodel);
                    Vector[] dVtx = new Vector[] { base._vtx[0][0], new Vector(base._vtx[0][0].X, base._vtx[0][0].Y, 0.0) };
                    base.DrawConSingle(dVtx, DrawGeometry.DefaultSection(base._sections[0].SecShape), ref fmodel);
                }
                IDrawGroup drawGroup = null;
                if (this._bfModel.GroupCount > 0)
                {
                    for (int i = 0; i < this._bfModel.GroupCount; i++)
                    {
                        drawGroup = this._bfModel.GetGroup(i);
                        fmodel.AddGroup(drawGroup);
                    }
                }
                if (this._bsModel != null)
                {
                    int[] numArray2 = new int[1];
                    base.NewEmptyModel(numArray2, base._renderType, renderInfo, out smodel);
                    IDrawPrimitive primitive2 = smodel.GetGroup(0).GetPrimitive(0);
                    primitive2.Material.CullMode = gviCullFaceMode.gviCullNone;
                    primitive2.VertexArray = primitive.VertexArray;
                    primitive2.NormalArray = primitive.NormalArray;
                    primitive2.TexcoordArray = primitive.TexcoordArray;
                    primitive2.IndexArray = primitive.IndexArray;
                    if (this._bsModel.GroupCount > 0)
                    {
                        for (int j = 0; j < this._bsModel.GroupCount; j++)
                        {
                            drawGroup = this._bsModel.GetGroup(j);
                            smodel.AddGroup(drawGroup);
                        }
                    }
                }
                return true;
            }
            catch (Exception exception)
            {
                return false;
            }
        }

        public void SetBaseModel(IModel bfModel, IModel bsModel)
        {
            this._bfModel = bfModel;
            this._bsModel = bsModel;
        }

        // Properties
        public IModel BaseFineModel
        {
            get
            {
                return this._bfModel;
            }
            set
            {
                this._bfModel = value;
            }
        }

        public IModel BaseSimpleModel
        {
            get
            {
                return this._bsModel;
            }
            set
            {
                this._bsModel = value;
            }
        }
    }

 

}
