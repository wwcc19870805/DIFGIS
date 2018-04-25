//============================================================================
// 绘制流向
//============================================================================
using DF3DPipeCreateTool.Class;
using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.Resource;
using System;
using System.Collections.Generic;
namespace DF3DPipeCreateTool.ParamModeling
{
    public class DrawDynamicFlow : DrawScanModel, IDrawDynamicFlow, IDrawScanModel, IDrawGeometry
    {
        // Fields
        protected int _flowDir;
        protected IPipeSection _pipeSection;
        protected List<Vector> _route;
        protected TurnerStyle _turnerStlye;

        // Methods
        public DrawDynamicFlow()
        {
            base._modeltype = ModelType.DynamicFlow;
        }

        public override bool Draw(out IModelPoint mp, out IModel fmodel, out IModel smodel)
        {
            List<ushort> list2 = new List<ushort>();
            base.Draw(out mp, out fmodel, out smodel);
            try
            {
                int num;
                int num2;
                List<Vector> list;
                if (this._turnerStlye == TurnerStyle.Capround)
                {
                    list = Maths.DisperseLine(this._route, this._pipeSection.Diameter);
                }
                else
                {
                    list = this._route;
                }
                double[] numArray2 = this._pipeSection.GetVtxs(1.05, 0.0, 0.0);
                double[] numArray = new double[list.Count];
                for (num = 0; num < list.Count; num++)
                {
                    if (num == 0)
                    {
                        numArray[num] = 0.0;
                    }
                    else
                    {
                        numArray[num] = (list[num] - list[num - 1]).Length + numArray[num - 1];
                    }
                }
                object renderInfo = (base._renderType == RenderType.Texture) ? ((object)base._tcNames) : ((object)base._colors);
                int[] index = new int[1];
                if (!base.NewEmptyModel(index, base._renderType, renderInfo, out fmodel))
                {
                    return false;
                }
                IDrawPrimitive primitive = null;
                primitive = fmodel.GetGroup(0).GetPrimitive(0);
                for (num = 0; num < list.Count; num++)
                {
                    Vector vector;
                    Vector vector2;
                    Vector vector3;
                    if (num == 0)
                    {
                        vector = list[num + 1] - list[num];
                    }
                    else if (num == (list.Count - 1))
                    {
                        vector = list[num] - list[num - 1];
                    }
                    else if (this._turnerStlye == TurnerStyle.Capround)
                    {
                        vector = list[num + 1] - list[num];
                    }
                    else
                    {
                        vector = ((list[num] - list[num - 1])).UnitVector() + ((list[num + 1] - list[num])).UnitVector();
                    }
                    Maths.GenerateComplementBasis(vector, out vector2, out vector3);
                    num2 = 0;
                    while (num2 <= this._pipeSection.SegCount)
                    {
                        Vector vector5 = (((numArray2[num2 * 4] - this._pipeSection.OffsetX) * vector2) + ((numArray2[(num2 * 4) + 1] - this._pipeSection.OffsetY) * vector3)).UnitVector();
                        Vector vector4 = list[num] + ((Vector)((numArray2[num2 * 4] * vector2) + (numArray2[(num2 * 4) + 1] * vector3)));
                        primitive.VertexArray.Append((float)vector4.X);
                        primitive.VertexArray.Append((float)vector4.Y);
                        primitive.VertexArray.Append((float)vector4.Z);
                        list2.Add((ushort)((primitive.VertexArray.Length / 3) - 1));
                        primitive.TexcoordArray.Append((float)(numArray[num] / (3.1415926535897931 * this._pipeSection.Diameter)));
                        primitive.TexcoordArray.Append((num2 * 1f) / ((float)this._pipeSection.SegCount));
                        primitive.NormalArray.Append((float)vector5.X);
                        primitive.NormalArray.Append((float)vector5.Y);
                        primitive.NormalArray.Append((float)vector5.Z);
                        num2++;
                    }
                }
                for (num = 0; num < (list.Count - 1); num++)
                {
                    for (num2 = 0; num2 < this._pipeSection.SegCount; num2++)
                    {
                        primitive.IndexArray.Append(list2[(num * (this._pipeSection.SegCount + 1)) + num2]);
                        primitive.IndexArray.Append(list2[(((num + 1) * (this._pipeSection.SegCount + 1)) + num2) + 1]);
                        primitive.IndexArray.Append(list2[((num + 1) * (this._pipeSection.SegCount + 1)) + num2]);
                        primitive.IndexArray.Append(list2[(num * (this._pipeSection.SegCount + 1)) + num2]);
                        primitive.IndexArray.Append(list2[((num * (this._pipeSection.SegCount + 1)) + num2) + 1]);
                        primitive.IndexArray.Append(list2[(((num + 1) * (this._pipeSection.SegCount + 1)) + num2) + 1]);
                    }
                }
                return true;
            }
            catch (Exception exception)
            {
                return false;
            }
        }

        public void SetParameter(IPipeSection pipeSection, IPolyline polyline, int flowDir)
        {
            try
            {
                this._pipeSection = pipeSection;
                base._cullModel = gviCullFaceMode.gviCullNone;
                DrawGeometry.GetPolylineVtxs(polyline, ref this._x, ref this._y, ref this._z, ref this._route);
                this._flowDir = flowDir;
                if (this._flowDir == 1)
                {
                    List<Vector> list = new List<Vector>();
                    for (int i = this._route.Count - 1; i >= 0; i--)
                    {
                        list.Add(this._route[i]);
                    }
                    this._route = list;
                }
            }
            catch (Exception exception)
            {
            }
        }

        // Properties
        public int FlowDrection
        {
            get
            {
                return this._flowDir;
            }
            set
            {
                this._flowDir = value;
            }
        }

        public IPipeSection PipeSection
        {
            get
            {
                return this._pipeSection;
            }
            set
            {
                this._pipeSection = value;
            }
        }

        public List<Vector> Route
        {
            get
            {
                return this._route;
            }
            set
            {
                this._route = value;
            }
        }

        public TurnerStyle TurnerStyle
        {
            get
            {
                return this._turnerStlye;
            }
            set
            {
                this._turnerStlye = value;
            }
        }
    }

 

}
