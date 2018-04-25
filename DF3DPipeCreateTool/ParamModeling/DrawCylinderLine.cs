using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.Math;
using Gvitech.CityMaker.Resource;
using System;
using System.Collections.Generic;
using DF3DPipeCreateTool.Class;
namespace DF3DPipeCreateTool.ParamModeling
{
    public class DrawCylinderLine : DrawGeometry, IDrawCylinderLine, IDrawGeometry
    {
        // Fields
        protected uint _color;
        protected IPipeSection _pipeSection;
        protected List<List<Vector>> _route;
        protected string _tcName;

        // Methods
        public DrawCylinderLine()
        {
            this._tcName = "";
            this._color = 0xffff0000;
            base._modeltype = ModelType.CylinderLine;
        }

        public DrawCylinderLine(IMultiPolyline multiLine, IPipeSection section, string tcName, uint color)
        {
            this._tcName = "";
            this._color = 0xffff0000;
            base._modeltype = ModelType.CylinderLine;
            this._pipeSection = section;
            base._cullModel = gviCullFaceMode.gviCullNone;
            this._tcName = tcName;
            this._color = color;
            if (multiLine != null)
            {
                IEnvelope envelope = null;
                IPolyline polyline = null;
                List<Vector> item = null;
                IPoint point = null;
                Vector vector = null;
                envelope = multiLine.Envelope;
                base._x = (envelope.MaxX + envelope.MinX) / 2.0;
                base._y = (envelope.MaxY + envelope.MinY) / 2.0;
                base._z = (envelope.MaxZ + envelope.MinZ) / 2.0;
                this._route = new List<List<Vector>>();
                for (int i = 0; i < multiLine.GeometryCount; i++)
                {
                    polyline = multiLine.GetPolyline(i);
                    item = new List<Vector>();
                    for (int j = 0; j < polyline.PointCount; j++)
                    {
                        point = polyline.GetPoint(j);
                        vector = new Vector(point.X - base._x, point.Y - base._y, point.Z - base._z);
                        item.Add(vector);
                    }
                    this._route.Add(item);
                }
            }
        }

        public override bool Draw(out IModelPoint mp, out IModel fmodel, out IModel smodel)
        {
            List<ushort> list2 = new List<ushort>();
            base.Draw(out mp, out fmodel, out smodel);
            try
            {
                double[] vtxs;
                int num3;
                if ((this._route != null) && (this._route.Count != 0))
                {
                    vtxs = this._pipeSection.GetVtxs();
                    int[] index = new int[1];
                    if (base.NewEmptyModel(index, RenderType.Texture, new string[] { this._tcName }, out fmodel))
                    {
                        int[] numArray4 = new int[1];
                        if (base.NewEmptyModel(numArray4, RenderType.Color, new uint[] { this._color }, out smodel))
                        {
                            goto Label_0093;
                        }
                    }
                }
                return false;
            Label_0093:
                num3 = 0;
                int num4 = 0;
                foreach (List<Vector> list3 in this._route)
                {
                    int num2;
                    List<Vector> list = list3;
                    double[] numArray = new double[list.Count];
                    int num = 0;
                    while (num < list.Count)
                    {
                        if (num == 0)
                        {
                            numArray[num] = 0.0;
                        }
                        else
                        {
                            numArray[num] = (list[num] - list[num - 1]).Length + numArray[num - 1];
                        }
                        num++;
                    }
                    IDrawPrimitive primitive = fmodel.GetGroup(0).GetPrimitive(0);
                    num = 0;
                    while (num < list.Count)
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
                        else
                        {
                            vector = ((list[num] - list[num - 1])).UnitVector() + ((list[num + 1] - list[num])).UnitVector();
                        }
                        Maths.GenerateComplementBasis(vector, out vector2, out vector3);
                        num2 = 0;
                        while (num2 <= this._pipeSection.SegCount)
                        {
                            Vector vector5 = (((vtxs[num2 * 4] - this._pipeSection.OffsetX) * vector2) + ((vtxs[(num2 * 4) + 1] - this._pipeSection.OffsetY) * vector3)).UnitVector();
                            Vector vector4 = list[num] + ((Vector)((vtxs[num2 * 4] * vector2) + (vtxs[(num2 * 4) + 1] * vector3)));
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
                        num++;
                    }
                    for (num = 0; num < (list.Count - 1); num++)
                    {
                        for (num2 = 0; num2 < this._pipeSection.SegCount; num2++)
                        {
                            primitive.IndexArray.Append((ushort)(num3 + list2[(num * (this._pipeSection.SegCount + 1)) + num2]));
                            primitive.IndexArray.Append((ushort)(num3 + list2[(((num + 1) * (this._pipeSection.SegCount + 1)) + num2) + 1]));
                            primitive.IndexArray.Append((ushort)(num3 + list2[((num + 1) * (this._pipeSection.SegCount + 1)) + num2]));
                            primitive.IndexArray.Append((ushort)(num3 + list2[(num * (this._pipeSection.SegCount + 1)) + num2]));
                            primitive.IndexArray.Append((ushort)(num3 + list2[((num * (this._pipeSection.SegCount + 1)) + num2) + 1]));
                            primitive.IndexArray.Append((ushort)(num3 + list2[(((num + 1) * (this._pipeSection.SegCount + 1)) + num2) + 1]));
                        }
                    }
                    IDrawPrimitive primitive2 = smodel.GetGroup(0).GetPrimitive(0);
                    primitive2.PrimitiveMode = gviPrimitiveMode.gviPrimitiveModeLineList;
                    for (int i = 0; i < (list.Count - 1); i++)
                    {
                        Vector vector6 = list[i];
                        Vector vector7 = list[i + 1];
                        primitive2.VertexArray.Append((float)vector6.X);
                        primitive2.VertexArray.Append((float)vector6.Y);
                        primitive2.VertexArray.Append((float)vector6.Z);
                        primitive2.IndexArray.Append((ushort)(num4 + (i * 2)));
                        primitive2.VertexArray.Append((float)vector7.X);
                        primitive2.VertexArray.Append((float)vector7.Y);
                        primitive2.VertexArray.Append((float)vector7.Z);
                        primitive2.IndexArray.Append((ushort)((num4 + (i * 2)) + 1));
                        primitive2.NormalArray.Append(0f);
                        primitive2.NormalArray.Append(0f);
                        primitive2.NormalArray.Append(1f);
                        primitive2.NormalArray.Append(0f);
                        primitive2.NormalArray.Append(0f);
                        primitive2.NormalArray.Append(1f);
                    }
                    num3 += list.Count * (this._pipeSection.SegCount + 1);
                    num4 += list.Count;
                }
                fmodel.AddGroup(smodel.GetGroup(0));
                return true;
            }
            catch (Exception exception)
            {
                return false;
            }
        }

        // Properties
        public uint Color
        {
            get
            {
                return this._color;
            }
            set
            {
                this._color = value;
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

        public List<List<Vector>> Route
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

        public string TcName
        {
            get
            {
                return this._tcName;
            }
            set
            {
                this._tcName = value;
            }
        }
    }

 

}
