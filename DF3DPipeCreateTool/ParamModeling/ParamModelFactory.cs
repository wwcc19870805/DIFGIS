using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DF3DPipeCreateTool.Class;

namespace DF3DPipeCreateTool.ParamModeling
{
    public class ParamModelFactory
    {
        private static ParamModelFactory _instance;
        private static readonly object syncRoot = new object();
        private ParamModelFactory()
        {
        }
        public static ParamModelFactory Instance
        {
            get
            {
                if (ParamModelFactory._instance == null)
                {
                    lock (syncRoot)
                    {
                        if (ParamModelFactory._instance == null)
                        {
                            ParamModelFactory._instance = new ParamModelFactory();
                        }
                    }
                }
                return ParamModelFactory._instance;
            }
        }
       
        public IDrawGeometry CreateGeometryDraw(ModelType modeltype, string modelname)
        {
            IDrawGeometry geometry = null;
            switch (modeltype)
            {
                case ModelType.StaticModel:
                    geometry = new DrawStaticModel();
                    break;

                case ModelType.PipeLine:
                    geometry = new DrawPipeLine();
                    break;

                case ModelType.JointEnd:
                    geometry = new DrawJointEnd();
                    break;

                case ModelType.JointTwo:
                    geometry = new DrawJointTwo();
                    break;

                case ModelType.JointThree:
                    geometry = new DrawJointThree();
                    break;

                case ModelType.JointFour:
                    geometry = new DrawJointFour();
                    break;

                case ModelType.JointMulti:
                    geometry = new DrawJointMulti();
                    break;

                case ModelType.HeevenWell:
                    geometry = new DrawHeevenWell();
                    break;

                case ModelType.ReduceWell:
                    geometry = new DrawReduceWell();
                    break;

                case ModelType.TerrainWell:
                    geometry = new DrawTerrainWell();
                    break;

                case ModelType.ConnBlend:
                    geometry = new DrawConnBlend();
                    break;

                case ModelType.DynamicFlow:
                    geometry = new DrawDynamicFlow();
                    break;
            }
            if (geometry != null)
            {
                geometry.ModelName = modelname;
            }
            return geometry;
        }
    }
}
