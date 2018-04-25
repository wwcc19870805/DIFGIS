using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Display;
using System.Windows.Forms;
using System.Drawing;
using DevExpress.XtraTreeList;
using DF2DControl.Base;
using ICSharpCode.Core;
using DFWinForms.LogicTree;
using DF2DData.Tree;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.Utils;
using DFCommon.Class;
using DFDataConfig.Logic;
using DF2DData.LogicTree;
using ESRI.ArcGIS.Controls;
using System.IO;
using DevExpress.XtraSplashScreen;



namespace DF2DData.Class
{
    public static class DataUtils2D
    {
        
        #region 【原数据结构】管线数据添加并可视化
        private static TreeNodeComLayer rootNode = null;
        public static void AddAndVisualizeTreelistBackground(TreeList parentTree,IMapControl2 mapControl1)
        {
            try
            {
                if (mapControl1 == null) return;
                IMapDocument pMapDoc = new MapDocument();
                string pFileName = Config.GetConfigValue("2DMxdBackground");//获得背景图文件路径 
                if(string.IsNullOrEmpty(pFileName)||!File.Exists(pFileName)) return;
                SplashScreenManager.Default.SendCommand(null, "正在加载二维基础数据......");
                pMapDoc.Open(pFileName, null);//打开地图
                IMap pMap = pMapDoc.get_Map(0);//获得地图
                List<ILayer> loadLayer = new List<ILayer>();//初始化待加载地理信息图层list
                int layerNum = mapControl1.Map.LayerCount;//获得当前主地图控件的图层数               
                for (int i = 0; i < pMap.LayerCount; i++)//对地图的图层进行遍历
                {
                    ILayer layer = pMap.get_Layer(i);                    
                    AddComLayerNode(parentTree, rootNode, layer,loadLayer);//为图层树控件加载新图层，并将待加载图层写入loadLayer
                    rootNode.CollapseAll();

                }
                if (loadLayer.Count > 0)
                {
                    foreach(ILayer layer in loadLayer)
                    {
                        mapControl1.AddLayer(layer, layerNum);
                        layerNum++;
                        //if (layer is IRasterLayer)
                        //{
                        //    mapControl2.AddLayer(layer);
                        //}
                    }
                    IEnvelope pEnv = mapControl1.ActiveView.Extent;
                    IPoint point = new ESRI.ArcGIS.Geometry.Point();
                    point.PutCoords((pEnv.XMax + pEnv.XMin) / 2, (pEnv.YMax + pEnv.YMin) / 2);

                    pEnv.CenterAt(point);
                    mapControl1.ActiveView.Extent = pEnv;
                    mapControl1.MapUnits = pMap.MapUnits;
                    mapControl1.ReferenceScale = pMap.ReferenceScale;
                    mapControl1.ActiveView.Refresh();
                }
            }
            catch (System.Exception ex)
            {
                return;
            }
        }
        public static void AddAndVisualizeEgleEyeControlBackground(AxMapControl mapControl2)
        {
            try
            {
                if (mapControl2 == null) return;
                IMapDocument pMapDoc = new MapDocument();
                string pFileName = Config.GetConfigValue("2DMxdEgleEye");//获得背景图文件路径 
                if(string.IsNullOrEmpty(pFileName)||!File.Exists(pFileName)) return;
                SplashScreenManager.Default.SendCommand(null, "正在加载二维缩略图数据......");
                pMapDoc.Open(pFileName, null);//打开地图
                IMap pMap = pMapDoc.get_Map(0);//获得地图
                for(int i =0;i<pMap.LayerCount;i++)
                {
                    ILayer layer = pMap.get_Layer(i);
                    if(layer is IRasterLayer) {mapControl2.AddLayer(layer);break;}
                }
            }
            catch (System.Exception ex)
            {
            	return;
            }
        }

        public static void AddComLayerNode(TreeList parentTree, TreeNodeComLayer node, ILayer layer,List<ILayer> loadlayer)
        {
            object renderer = null;
            TreeNodeComLayer comLayerNode;
            if (layer is ICadLayer) return;
            if (node == null)//如果当前节点为空
            {
                comLayerNode = new TreeNodeComLayer() { Name = layer.Name, CustomValue = layer };
                comLayerNode.OwnNode = parentTree.AppendNode(new object[] { comLayerNode.Name }, (TreeListNode)null);//为树创建根节点
                comLayerNode.ImageIndex = 0;
                node = comLayerNode;
                rootNode = comLayerNode;
                rootNode.Visible = true;
                
            }
            else
            {
                comLayerNode = new TreeNodeComLayer() { Name = layer.Name, CustomValue = layer };
                comLayerNode.Visible = true;
                if (comLayerNode.Name.Contains("注记"))
                {
                    comLayerNode.ImageIndex = 10;
                }
                else
                {
                    comLayerNode.ImageIndex = 0;
                }
                node.Add(comLayerNode);//为当前节点添加子节点

            }
            if (layer is ESRI.ArcGIS.Carto.IGroupLayer)//如果图层是复合图层组
            {

                ICompositeLayer comLayer = layer as ICompositeLayer;
                for (int i = 0; i < comLayer.Count; i++)
                {
                    AddComLayerNode(parentTree, comLayerNode, comLayer.get_Layer(i),loadlayer);//递归
                    comLayerNode.CollapseAll();
                }

            }            
            else if (layer is IGeoFeatureLayer)//如果图层是地理要素图层
            {                
                loadlayer.Add(layer);//将该图层添加到待加载图层
                IGeoFeatureLayer geoFtLayer = layer as IGeoFeatureLayer;
        
                if (geoFtLayer == null) return;
                if (geoFtLayer.FeatureClass == null)
                { node.CollapseAll(); return; }
                esriGeometryType geoType = geoFtLayer.FeatureClass.ShapeType;
                switch (geoType)
                {
                    case esriGeometryType.esriGeometryPoint:
                    case esriGeometryType.esriGeometryMultipoint:
                        comLayerNode.ImageIndex = 4;
                        break;
                    case esriGeometryType.esriGeometryLine:
                    case esriGeometryType.esriGeometryPolyline:
                        comLayerNode.ImageIndex = 5;
                        break;
                    case esriGeometryType.esriGeometryPolygon:
                        comLayerNode.ImageIndex = 6;
                        break;

                }    
                renderer = geoFtLayer.Renderer;
                
                AddSymbolNode(comLayerNode, renderer);//对当前树节点进行符号化
                node.CollapseAll();
                comLayerNode.CollapseAll();
                if (geoFtLayer.FeatureClass == null) return;
                DF2DFeatureClass dffc = new DF2DFeatureClass(geoFtLayer.FeatureClass,comLayerNode);//根据当前要素类创建DF2DFC，并添加到管理类
                dffc.SetLayer(layer);
                DF2DFeatureClassManager.Instance.Add(dffc);
                DF2DFeatureClassManager.Instance.Add(comLayerNode);
            }

            else if (layer is IRasterLayer)//如果图层是栅格图层
            {
                loadlayer.Add(layer);//将该图层添加到待加载图层
                //comLayerNode.Visible = true; //当前图层为可见（显示影像图）
                IRasterLayer rasterLayer = layer as IRasterLayer;
                IRaster raster = rasterLayer.Raster;
                if(raster == null) return;
                DF2DRaster dfrd = new DF2DRaster(raster, comLayerNode);
                dfrd.SetLayer(layer);
                DF2DRasterManager.Instance.Add(dfrd);
            }
            else if (layer is IAnnotationLayer)
            {
                loadlayer.Add(layer);
                IFeatureLayer featureLayer = layer as IFeatureLayer;
                if (featureLayer.FeatureClass == null) return;
                DF2DFeatureClass dffc = new DF2DFeatureClass(featureLayer.FeatureClass, comLayerNode);
                dffc.SetLayer(layer);
                DF2DFeatureClassManager.Instance.Add(dffc);
                DF2DFeatureClassManager.Instance.Add(comLayerNode);
                
            }
          
           
            else
            {
                
                loadlayer.Add(layer);//其它图层也加入待加载图层
            }
            

        }
        private static void AddSymbolNode(TreeNodeComLayer tnComLayer, object renderer)
        {
            int index;
            LogicBaseTree logicBT = new LogicBaseTree();
            ImageCollection treeImages = logicBT.TreeList.StateImageList as ImageCollection;
            if (renderer is SimpleRenderer)
            {
                ISimpleRenderer simpleRenderer = (ISimpleRenderer)renderer;
                ISymbol symbol = simpleRenderer.Symbol;
                Image img = ConvertSymbolToImage.Convert(symbol, 16, 16);             
                index = treeImages.Images.Add(img);
                //img.Save("d:\\" + index + ".JPG");

                string label = simpleRenderer.Label;
                TreeNodeSymbol tnSymbol = new TreeNodeSymbol() { Name = label, CustomValue = symbol };
                tnSymbol.ImageIndex = index;
                tnComLayer.Add(tnSymbol);
            }
            else if (renderer is UniqueValueRenderer)
            {
                string renderLabel = "";
                IUniqueValueRenderer uniqueValueRenderer = renderer as IUniqueValueRenderer;
                for (int i = 0; i < uniqueValueRenderer.FieldCount - 1; i++)
                {
                    renderLabel += uniqueValueRenderer.get_Field(i) + " /";
                }
                renderLabel += uniqueValueRenderer.get_Field(uniqueValueRenderer.FieldCount - 1);//FieldCount为地物分类数
                //getImage(esriGeometryType.esriGeometryPolygon, Color.White).Save("D:\\111.jpg");
                index = treeImages.Images.Add(getImage(esriGeometryType.esriGeometryPolygon, Color.White));

                TreeNodeSymbol tnChartNode = new TreeNodeSymbol() { Name = renderLabel, CustomValue = uniqueValueRenderer };
                tnChartNode.ImageIndex = index;
                tnComLayer.Add(tnChartNode);

                for (int i = 0; i < uniqueValueRenderer.ValueCount; i++)//ValueCount为地物分类下的设施种类数
                {
                    ISymbol symbol = uniqueValueRenderer.get_Symbol(uniqueValueRenderer.get_Value(i));

                    Image img = ConvertSymbolToImage.Convert(symbol, 16, 16);
                    //img.Save("D:\\121121.jpg");


                    index = treeImages.Images.Add(img);

                    TreeNodeSymbol tnSymbol = new TreeNodeSymbol() { Name = uniqueValueRenderer.get_Label(uniqueValueRenderer.get_Value(i)), CustomValue = symbol };
                    tnSymbol.ImageIndex = index;
                    tnComLayer.Add(tnSymbol);

                }
                if (uniqueValueRenderer.UseDefaultSymbol)
                {
                    ISymbol symbol = uniqueValueRenderer.DefaultSymbol;
                    Image img = ConvertSymbolToImage.Convert(symbol, 16, 16);
                    index = treeImages.Images.Add(img);

                    TreeNodeSymbol tnSymbol = new TreeNodeSymbol() { Name = uniqueValueRenderer.DefaultLabel, CustomValue = symbol };
                    tnSymbol.ImageIndex = index;
                    tnComLayer.Add(tnSymbol);
                }


            }
            else if (renderer is IChartRenderer)
            {
                IChartRenderer chartRenderer = renderer as IChartRenderer;
                IRendererFields pFields = chartRenderer as IRendererFields;
                string renderLabel = "";
                for (int i = 0; i < pFields.FieldCount - 1; i++)
                {
                    renderLabel += pFields.get_FieldAlias(i) + " /";
                }
                renderLabel += pFields.get_FieldAlias(pFields.FieldCount - 1);

                index = treeImages.Images.Add(getImage(esriGeometryType.esriGeometryPolygon, Color.White));

                TreeNodeSymbol tnChartNode = new TreeNodeSymbol() { Name = renderLabel, CustomValue = chartRenderer };
                tnChartNode.ImageIndex = index;
                tnComLayer.Add(tnChartNode);


                ISymbolArray symArray = chartRenderer.ChartSymbol as ISymbolArray;
                for (int i = 0; i < symArray.SymbolCount; i++)
                {
                    ISymbol symbol = symArray.get_Symbol(i);

                    Image img = ConvertSymbolToImage.Convert(symbol, 16, 16);
                    index = treeImages.Images.Add(img);

                    TreeNodeSymbol tnSymbol = new TreeNodeSymbol() { Name = pFields.get_FieldAlias(i), CustomValue = symbol };
                    tnSymbol.ImageIndex = index;
                    tnComLayer.Add(tnSymbol);
                }
            }
            else if (renderer is IClassBreaksRenderer)
            {
                IClassBreaksRenderer classRenderer = renderer as IClassBreaksRenderer;
                index = treeImages.Images.Add(getImage(esriGeometryType.esriGeometryPolygon, Color.White));

                TreeNodeSymbol tnChartNode = new TreeNodeSymbol() { Name = classRenderer.Field, CustomValue = classRenderer };
                tnChartNode.ImageIndex = index;
                tnComLayer.Add(tnChartNode);


                for (int i = 0; i < classRenderer.BreakCount; i++)
                {
                    ISymbol symbol = classRenderer.get_Symbol(i);

                    Image img = ConvertSymbolToImage.Convert(symbol, 16, 16);
                    index = treeImages.Images.Add(img);

                    TreeNodeSymbol tnSymbol = new TreeNodeSymbol() { Name = classRenderer.get_Label(i), CustomValue = symbol };
                    tnSymbol.ImageIndex = index;
                    tnComLayer.Add(tnSymbol);


                }
            }
            else if (renderer is IRasterRenderer)
            {
                if (renderer is IRasterClassifyColorRampRenderer)
                {
                    //MessageBox.Show("IRasterClassifyColorRampRenderer");
                }
                else if (renderer is IRasterUniqueValueRenderer)
                {
                    //MessageBox.Show("IRasterUniqueValueRenderer");
                }
                else if (renderer is IRasterStretchColorRampRenderer)
                {
                    ////MessageBox.Show("IRasterStretchColorRampRenderer");
                    IRasterStretchColorRampRenderer pRSCRR = renderer as IRasterStretchColorRampRenderer;

                    index = treeImages.Images.Add(getImage(esriGeometryType.esriGeometryPolygon, Color.White));
                    TreeNodeSymbol tnChartNode = new TreeNodeSymbol() { Name = "<Value>", CustomValue = pRSCRR };
                    tnChartNode.ImageIndex = index;
                    tnComLayer.Add(tnChartNode);

                    if (pRSCRR.ColorRamp.Size >= 3)
                    {
                        IEnumColors colors = pRSCRR.ColorRamp.Colors;
                        colors.Reset();
                        IColor c = colors.Next();
                        Color[] cArray = new Color[3];
                        int count = 0;
                        while (c != null)
                        {
                            count++;
                            if (count == 1)
                            {
                                cArray[0] = Color.FromArgb(c.RGB);
                            }
                            else if (count == pRSCRR.ColorRamp.Size / 2)
                            {
                                cArray[1] = Color.FromArgb(c.RGB);
                            }
                            else if (count == pRSCRR.ColorRamp.Size)
                            {
                                cArray[2] = Color.FromArgb(c.RGB);
                            }
                            c = colors.Next();
                        }
                        for (int i = 0; i < 3; i++)
                        {
                            Image img = getImage(esriGeometryType.esriGeometryPolygon, cArray[i]);
                            index = treeImages.Images.Add(img);

                            string label = "";
                            if (i == 0)
                            {
                                label = pRSCRR.LabelLow;
                            }
                            else if (i == 1)
                            {
                                label = pRSCRR.LabelMedium;
                            }
                            else if (i == 2)
                            {
                                label = pRSCRR.LabelHigh;
                            }

                            TreeNodeSymbol tnSymbol = new TreeNodeSymbol() { Name = label, CustomValue = cArray[i] };
                            tnSymbol.ImageIndex = index;
                            tnComLayer.Add(tnSymbol);

                        }
                    }
                }

                else if (renderer is IRasterRGBRenderer)
                {
                    //MessageBox.Show("IRasterRGBRenderer");
                }
                else if (renderer is IRasterColormap)
                {
                    //MessageBox.Show("IRasterColormap");
                }
                else
                {
                    //MessageBox.Show("未处理的IRasterRenderer类型：  " + renderer.GetType().FullName);
                }
            }
            else if (renderer is ITinRenderer)
            {

                if (renderer is ITinColorRampRenderer)
                {
                    ////MessageBox.Show("ITinColorRampRenderer");
                    ITinColorRampRenderer pTCRR = renderer as ITinColorRampRenderer;

                    index = treeImages.Images.Add(getImage(esriGeometryType.esriGeometryPolygon, Color.White));
                    TreeNodeSymbol tnChartNode = new TreeNodeSymbol() { Name = "Elevation", CustomValue = pTCRR };
                    tnChartNode.ImageIndex = index;
                    tnComLayer.Add(tnChartNode);



                    for (int i = 0; i < pTCRR.BreakCount; i++)
                    {
                        ISymbol symbol = pTCRR.get_Symbol(i);
                        Image img = ConvertSymbolToImage.Convert(symbol, 16, 16);
                        index = treeImages.Images.Add(img);
                        TreeNodeSymbol tnSymbol = new TreeNodeSymbol() { Name = pTCRR.get_Label(i), CustomValue = symbol };
                        tnSymbol.ImageIndex = index;
                        tnComLayer.Add(tnSymbol);

                    }
                }
                else if (renderer is ITinUniqueValueRenderer)
                {
                    ////MessageBox.Show("ITinUniqueValueRenderer");
                    ITinUniqueValueRenderer pTUVR = renderer as ITinUniqueValueRenderer;

                    index = treeImages.Images.Add(getImage(esriGeometryType.esriGeometryPolygon, Color.White));

                    TreeNodeSymbol tnChartNode = new TreeNodeSymbol() { Name = "Elevation", CustomValue = pTUVR };
                    tnChartNode.ImageIndex = index;
                    tnComLayer.Add(tnChartNode);


                    for (int i = 0; i < pTUVR.ValueCount; i++)
                    {
                        string val = pTUVR.get_Value(i);
                        ISymbol symbol = pTUVR.get_Symbol(val);
                        Image img = ConvertSymbolToImage.Convert(symbol, 16, 16);
                        index = treeImages.Images.Add(img);

                        TreeNodeSymbol tnSymbol = new TreeNodeSymbol() { Name = pTUVR.get_Label(val), CustomValue = symbol };
                        tnSymbol.ImageIndex = index;
                        tnComLayer.Add(tnSymbol);


                    }
                    if (pTUVR.UseDefaultSymbol)
                    {
                        ISymbol symbol = pTUVR.DefaultSymbol;
                        Image img = ConvertSymbolToImage.Convert(symbol, 16, 16);
                        index = treeImages.Images.Add(img);

                        TreeNodeSymbol tnSymbol = new TreeNodeSymbol() { Name = pTUVR.DefaultLabel, CustomValue = symbol };
                        tnSymbol.ImageIndex = index;
                        tnComLayer.Add(tnSymbol);

                    }
                }
                else if (renderer is ITinSingleSymbolRenderer)
                {
                    ////MessageBox.Show("ITinSingleSymbolRenderer");
                    ITinSingleSymbolRenderer pTSSR = renderer as ITinSingleSymbolRenderer;
                    ISymbol symbol = pTSSR.Symbol;
                    Image img = ConvertSymbolToImage.Convert(symbol, 16, 16);
                    index = treeImages.Images.Add(img);

                    TreeNodeSymbol tnSymbol = new TreeNodeSymbol() { Name = pTSSR.Label, CustomValue = symbol };
                    tnSymbol.ImageIndex = index;
                    tnComLayer.Add(tnSymbol);


                }
                else
                {
                    //MessageBox.Show("未处理的ITinRenderer类型：  " + renderer.GetType().FullName);
                }
            }
            else
            {
                //MessageBox.Show("未处理的Renderer类型：  " + renderer.GetType().FullName);
            }

        }

        #endregion

      
        #region 【逻辑分组】管线数据添加并可视化
        
        public static void AddAndVisualizeTreelistPipe(TreeList parentTree,IMapControl2 mapControl)
        {
            try
            {
                
                if (parentTree == null) return;
                IMapDocument pMapDoc = new MapDocument();
                string pFileName = Config.GetConfigValue("2DMxdPipe");
                if(string.IsNullOrEmpty(pFileName)||!File.Exists(pFileName)) return;
                SplashScreenManager.Default.SendCommand(null, "正在加载二维管线数据......");
                pMapDoc.Open(pFileName, null);
                IMap pMap = pMapDoc.get_Map(0);
                Dictionary<string, IFeatureClass> dicfc = new Dictionary<string, IFeatureClass>();//初始化要<素类ID,要素类>字典
                for (int i = 0; i < pMap.LayerCount; i++)//对地图图层进行遍历
                {
                    ILayer layer = pMap.get_Layer(i);
                    ReadMapLayer(layer, dicfc);//读取该图层，并更新字典
                }
                if (dicfc == null || dicfc.Count == 0) return;
                foreach (LogicGroup lg in LogicDataStructureManage2D.Instance.RootLogicGroups)
                {
                    TreeNodeLogicGroup2D tnlg = new TreeNodeLogicGroup2D()//创建逻辑分组根节点
                    {
                        Name = string.IsNullOrEmpty(lg.Alias) ? lg.Name : lg.Alias
                    };
                    tnlg.OwnNode = parentTree.AppendNode(new object[] { tnlg.Name }, (TreeListNode)null);
                    tnlg.Visible = true;
                    RecursiveAddAndVisualizePipeData(tnlg.OwnNode.TreeList, lg.LogicGroups, dicfc,mapControl,pMap);
                    foreach (MajorClass mc in lg.MajorClasses)//遍历当前逻辑分组的二级大类
                    {
                        Dictionary<string, DF2DFeatureClass> dict = new Dictionary<string, DF2DFeatureClass>();//MajorClass所对应的<要素类ID,类>字典
                        Dictionary<string, IFeatureLayer> dicFLs = new Dictionary<string, IFeatureLayer>();//MajorClass所对应的<要素类ID,图层>字典
                        TreeNodeMajorClass2D tnmc = new TreeNodeMajorClass2D()
                        {
                            Name = string.IsNullOrEmpty(mc.Alias) ? mc.Name : mc.Alias
                        };
                        tnmc.Visible = true;
                        tnmc.CustomValue = mc;
                        tnlg.Add(tnmc);
                        SetFeatureClassForGroup(mc, tnmc, dicfc, dict);//设置MajorClass所对应的要素类
                        VisualMapLayer(pMap, mapControl, dict,dicFLs);//为地图控件加载相关的图层
                        SetSubClass(mc, tnmc, dict, dicFLs);//设置subclass包含的相关参数的值
                        //AddSubClass(mc, tnmc, dicfc,dict,dicFLs);                     

                        DF2DMajorClass dfmc = new DF2DMajorClass(mc, tnmc);//应用于管线显示控制
                        DF2DMajorClassManager.Instance.Add(dfmc);
                    }
                    tnlg.CollapseAll();
                }
            }
            catch (System.Exception ex)
            {
                return;
            }
        }
        private static void SetFeatureClassForGroup(MajorClass mc,DFWinForms.LogicTree.GroupLayerClass group,Dictionary<string, IFeatureClass> dicfc, Dictionary<string, DF2DFeatureClass> dict)
        {
            if (mc == null || string.IsNullOrEmpty(mc.ClassifyField) || string.IsNullOrEmpty(mc.Fc2D)) return;
            string[] arrayFc2D = mc.Fc2D.Split(';');
            if (arrayFc2D == null || arrayFc2D.Count() == 0) return;
            dict.Clear();
            foreach (string fc2D in arrayFc2D)
            {
                foreach (KeyValuePair<string, IFeatureClass> kv in dicfc)//在<要素类ID，要素类>字典里找到MajorClass对应的要素类，添加到dict中
                {
                    if (kv.Key == fc2D)
                    {
                        dict[kv.Key] = new DF2DFeatureClass(kv.Value);
                        DF2DFeatureClassManager.Instance.Add(dict[kv.Key]);
                        break;
                    }
                }
            }
            if (group is TreeNodeMajorClass2D) (group as TreeNodeMajorClass2D).FeatureClasses = dict;//设置MajorClass的要素类为dict
        }
        private static void LoadMapLayer(ILayer layer, Dictionary<string,DF2DFeatureClass> dict,List<ILayer> loadlayer)
        {
            if (layer is ESRI.ArcGIS.Carto.IGroupLayer)//如果是图层组
            {
                ICompositeLayer comLayer = layer as ICompositeLayer;//将图层转化为复合图层
                for (int i = 0; i < comLayer.Count; i++)
                {
                    ILayer lyr = comLayer.get_Layer(i);
                    LoadMapLayer(lyr, dict,loadlayer);
                }
            }
            if (layer is IAnnotationLayer)//如果是注记图层
            {
                IAnnotationLayer annotationLayer = layer as IAnnotationLayer;
                IFeatureLayer featureLayer = annotationLayer as IFeatureLayer;
                IFeatureClass fc = featureLayer.FeatureClass;
                if (featureLayer != null && fc!= null)
                {
                    string fcID = fc.FeatureClassID.ToString();
                    foreach (KeyValuePair<string, DF2DFeatureClass> v in dict)//在该MajorClass对应的要素类集中找到它的FeatureClassID，将图层添加到loadlayer中
                    {
                        if (fcID == v.Key)
                        {
                            loadlayer.Add(layer);
                            v.Value.SetLayer(layer);
                            break;
                        }
                    }                   
                }
            }
            if (layer is IGeoFeatureLayer)//如果是地理要素图层
            {
                IGeoFeatureLayer geoFtLayer = layer as IGeoFeatureLayer;
                if (geoFtLayer == null) return;
                if (geoFtLayer.FeatureClass == null) return;
                IFeatureClass fc = geoFtLayer.FeatureClass;
                string fcID = fc.FeatureClassID.ToString();
                foreach (KeyValuePair<string, DF2DFeatureClass> v in dict)//在该MajorClass对应的要素类集中找到它的FeatureClassID，将图层添加到loadlayer中
                {
                    if (fcID == v.Key)
                    {
                        loadlayer.Add(layer);
                        v.Value.SetLayer(layer);
                        break;
                    }
                }
            }
        }
        private static void SetSubClass(MajorClass mc, DFWinForms.LogicTree.GroupLayerClass group, Dictionary<string, DF2DFeatureClass> dict, Dictionary<string, IFeatureLayer> dicFLs)
        {
            foreach (SubClass sc in mc.SubClasses)
            {
                TreeNodeSubClass2D tnsc = new TreeNodeSubClass2D()
                {
                    Name = sc.Name,
                    CustomValue = sc,
                    ClassifyField = mc.ClassifyField,
                    DictFLs = dicFLs
                };
                tnsc.FeatureClasses = dict;
                group.Add(tnsc);
                tnsc.Visible = true;
            }
            group.CollapseAll();
        }
        private static void RecursiveAddAndVisualizePipeData(TreeList parentTree, List<LogicGroup> list, Dictionary<string, IFeatureClass> dicfc,IMapControl2 mapControl,IMap pMap)
        {
            if (parentTree == null || list == null) return;
            foreach (LogicGroup lg in list)
            {
                TreeNodeLogicGroup2D tnlg = new TreeNodeLogicGroup2D()
                {
                    Name = string.IsNullOrEmpty(lg.Alias) ? lg.Name : lg.Alias
                };
                tnlg.OwnNode = parentTree.AppendNode(new object[] { tnlg.Name }, (TreeListNode)null);
                tnlg.Visible = true;
                RecursiveAddAndVisualizePipeData(tnlg.OwnNode.TreeList, lg.LogicGroups, dicfc,mapControl,pMap);
                foreach (MajorClass mc in lg.MajorClasses)
                {
                    Dictionary<string, DF2DFeatureClass> dict = new Dictionary<string, DF2DFeatureClass>();//subclass所对应的<要素类ID,类>字典
                    Dictionary<string, IFeatureLayer> dicFLs = new Dictionary<string, IFeatureLayer>();//subclass所对应的<要素类ID,图层>字典
                    TreeNodeMajorClass2D tnmc = new TreeNodeMajorClass2D()
                    {
                        Name = string.IsNullOrEmpty(mc.Alias) ? mc.Name : mc.Alias
                    };
                    tnmc.Visible = true;
                    tnmc.CustomValue = mc;
                    tnlg.Add(tnmc);
                    SetFeatureClassForGroup(mc, tnmc, dicfc, dict);
                    VisualMapLayer(pMap, mapControl, dict, dicFLs);
                    SetSubClass(mc, tnmc, dict, dicFLs);
                    //AddSubClass(mc, tnlg, fcs, dict, dicFLs);

                }
                tnlg.CollapseAll();
            }
        }    

        private static void VisualMapLayer(IMap map ,IMapControl2 mapControl, Dictionary<string, DF2DFeatureClass> dict,Dictionary<string, IFeatureLayer> dicFLs)
        {
            List<ILayer> loadLayer = new List<ILayer>();
            dicFLs.Clear();
            for (int i = 0; i < map.LayerCount; i++)
            {
                ILayer lyr = map.get_Layer(i);
                LoadMapLayer(lyr, dict, loadLayer);
            }
            if (loadLayer.Count > 0)
            {
                List<DF2DFeatureClass> ldffc = DF2DFeatureClassManager.Instance.GetAllFeatureClass();
                int layerNum = mapControl.LayerCount;//获得地图控件当前的图层数
                foreach (ILayer layer in loadLayer)
                {
                    foreach (DF2DFeatureClass dffc in ldffc)
                    {
                        if (layer is IGeoFeatureLayer)//如果图层是地理要素图层
                        {
                            IGeoFeatureLayer geoLayer = layer as IGeoFeatureLayer;
                            if (geoLayer.FeatureClass == dffc.GetFeatureClass())
                            {
                                dffc.SetLayer(geoLayer as ILayer);//为dffc设置featurelayer
                            }
                        }
                    }
                    dicFLs.Add(layer.Name, layer as IFeatureLayer);//将当前图层添加到dicFLs中
                    mapControl.AddLayer(layer, layerNum);//为地图控件加载当前图层
                    layerNum++;//地图控件图层数+1
                }
                IEnvelope pEnv = mapControl.ActiveView.Extent;
                IPoint point = new ESRI.ArcGIS.Geometry.Point();
                point.PutCoords((pEnv.XMax + pEnv.XMin) / 2, (pEnv.YMax + pEnv.YMin) / 2);
                pEnv.CenterAt(point);
                mapControl.ActiveView.Extent = pEnv;
                mapControl.MapUnits = map.MapUnits;
                mapControl.ReferenceScale = map.ReferenceScale;
                mapControl.ActiveView.Refresh();
            }
        }
        public static Dictionary<string, IFeatureClass> GetFeatureClass()
        {
            try
            {
                Dictionary<string, IFeatureClass> dicfc = new Dictionary<string, IFeatureClass>();
                IMapDocument pMapDocument = new MapDocument();
                string pFileName = Config.GetConfigValue("2DMxdPipe");
                pMapDocument.Open(pFileName, "");
                IMap map = pMapDocument.ActiveView.FocusMap;
                for (int i = 0; i < map.LayerCount; i++)
                {
                    ILayer layer = map.get_Layer(i);
                    ReadMapLayer(layer,dicfc);
                }
                return dicfc;        
            }
            catch (System.Exception ex)
            {
                return null;                
            }
            
        }
      
        private static void  ReadMapLayer(ILayer layer,Dictionary<string, IFeatureClass> dicfc)
        {
            
            if (layer is ESRI.ArcGIS.Carto.IGroupLayer)
            {
                ICompositeLayer comLayer = layer as ICompositeLayer;
                for (int i = 0; i < comLayer.Count; i++)
                {
                    ILayer lyr = comLayer.get_Layer(i);
                    ReadMapLayer(lyr,dicfc);
                }
            }
            if (layer is IGeoFeatureLayer)
            {
                IGeoFeatureLayer geoFtLayer = layer as IGeoFeatureLayer;
                if (geoFtLayer == null) return ;
                if (geoFtLayer.FeatureClass == null) return ;
                IFeatureClass fc = geoFtLayer.FeatureClass;
                string fcID = fc.FeatureClassID.ToString();
                dicfc.Add(fcID, fc);
            }
            if (layer is IAnnotationLayer)
            {
                IAnnotationLayer annotationLayer = layer as IAnnotationLayer;
                IFeatureLayer featureLayer = annotationLayer as IFeatureLayer;
                IFeatureClass fc = featureLayer.FeatureClass;
                if (featureLayer == null || fc == null) return;
                string fcID = fc.FeatureClassID.ToString();
                dicfc.Add(fcID, fc);          
            }
        }
        private static Image getImage(esriGeometryType geoType, Color c)
       {
           Bitmap bitmap = new Bitmap(16, 16, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
           Image img = bitmap;
           Graphics g = Graphics.FromImage(img);
           g.FillRectangle(new SolidBrush(Color.White), 0, 0, 16, 16);
           switch (geoType)
           {
               case esriGeometryType.esriGeometryPoint:
               case esriGeometryType.esriGeometryMultipoint:
                   bitmap.SetPixel(7, 5, c); bitmap.SetPixel(8, 8, c);
                   bitmap.SetPixel(7, 10, c); bitmap.SetPixel(8, 10, c);
                   bitmap.SetPixel(6, 6, c);
                   bitmap.SetPixel(7, 6, c);
                   bitmap.SetPixel(8, 6, c);
                   bitmap.SetPixel(9, 6, c);
                   bitmap.SetPixel(6, 9, c);
                   bitmap.SetPixel(7, 9, c);
                   bitmap.SetPixel(8, 9, c);
                   bitmap.SetPixel(9, 9, c);

                   bitmap.SetPixel(5, 7, c);
                   bitmap.SetPixel(6, 7, c);
                   bitmap.SetPixel(7, 7, c);
                   bitmap.SetPixel(8, 7, c);
                   bitmap.SetPixel(9, 7, c);
                   bitmap.SetPixel(10, 7, c);
                   bitmap.SetPixel(5, 8, c);
                   bitmap.SetPixel(6, 8, c);
                   bitmap.SetPixel(7, 8, c);
                   bitmap.SetPixel(8, 8, c);
                   bitmap.SetPixel(9, 8, c);
                   bitmap.SetPixel(10, 8, c);
                   break;
               case esriGeometryType.esriGeometryPolyline:
                   for (int i = 0; i < 16; i++)
                   {
                       for (int j = 7; j < 9; j++)
                       {
                           bitmap.SetPixel(i, j, c);
                       }
                   }
                   break;
               case esriGeometryType.esriGeometryPolygon:
                   for (int i = 0; i < 16; i++)
                   {
                       for (int j = 0; j < 16; j++)
                       {
                           bitmap.SetPixel(i, j, c);
                       }
                   }
                   break;
           }
           Image imgResult = bitmap;
           return imgResult;
       }
      


        #region 废弃代码
        //private static void AddSubClass(MajorClass mc, DFWinForms.LogicTree.GroupLayerClass group, Dictionary<string, IFeatureClass> fcs, Dictionary<string, DF2DFeatureClass> dict, Dictionary<string, IFeatureLayer> dicFLs)
        //{

        //    if (mc == null || string.IsNullOrEmpty(mc.ClassifyField) || string.IsNullOrEmpty(mc.Fc2D)) return;
        //    string[] arrayFc2D = mc.Fc2D.Split(';');
        //    if (arrayFc2D == null || arrayFc2D.Count() == 0) return;
        //    dict.Clear();
        //    dicFLs.Clear();
        //    // 给每个大类节点关联上所有的要素类，并记录上所有要素类
        //    //Dictionary<string, DF2DFeatureClass> dict = new Dictionary<string, DF2DFeatureClass>();
        //    //Dictionary<string, IFeatureLayer> dicFLs = new Dictionary<string, IFeatureLayer>();
        //    foreach (string fc2D in arrayFc2D)
        //    {
        //        foreach (KeyValuePair<string, IFeatureClass> kv in fcs)
        //        {
        //            if (kv.Key == fc2D)
        //            {
        //                dict[kv.Key] = new DF2DFeatureClass(kv.Value);
        //                DF2DFeatureClassManager.Instance.Add(dict[kv.Key]);
        //                break;
        //            }
        //        }
        //    }
        //    if (group is TreeNodeMajorClass2D) (group as TreeNodeMajorClass2D).FeatureClasses = dict;
        //    //可视化所有可见类



        //    List<ILayer> loadLayer = new List<ILayer>();

        //    for (int i = 0; i < map.LayerCount; i++)
        //    {
        //        ILayer lyr = map.get_Layer(i);
        //        LoadMapLayer(lyr, dict, loadLayer);
        //    }
        //    if (loadLayer.Count > 0)
        //    {
        //        List<DF2DFeatureClass> ldffc = DF2DFeatureClassManager.Instance.GetAllFeatureClass();
        //        int layerNum = mapControl.LayerCount;
        //        foreach (ILayer layer in loadLayer)
        //        {
        //            foreach (DF2DFeatureClass dffc in ldffc)
        //            {
        //                if (layer is IGeoFeatureLayer)
        //                {
        //                    IGeoFeatureLayer geoLayer = layer as IGeoFeatureLayer;
        //                    if (geoLayer.FeatureClass == dffc.GetFeatureClass())
        //                    {
        //                        dffc.SetFeatureLayer(geoLayer as IFeatureLayer);
        //                    }
        //                }
        //            }
        //            dicFLs.Add(layer.Name, layer as IFeatureLayer);
        //            mapControl.AddLayer(layer, layerNum);
        //            layerNum++;
        //        }
        //        IEnvelope pEnv = mapControl.ActiveView.Extent;
        //        IPoint point = new ESRI.ArcGIS.Geometry.Point();
        //        point.PutCoords((pEnv.XMax + pEnv.XMin) / 2, (pEnv.YMax + pEnv.YMin) / 2);
        //        pEnv.CenterAt(point);
        //        mapControl.ActiveView.Extent = pEnv;
        //        mapControl.MapUnits = map.MapUnits;
        //        mapControl.ReferenceScale = map.ReferenceScale;
        //        mapControl.ActiveView.Refresh();
        //    }

        //    // 给每个大类节点划分二级子类
        //    foreach (SubClass sc in mc.SubClasses)
        //    {
        //        TreeNodeSubClass2D tnsc = new TreeNodeSubClass2D()
        //        {
        //            Name = sc.Name,
        //            CustomValue = sc,
        //            ClassifyField = mc.ClassifyField,
        //            DictFLs = dicFLs
        //        };
        //        tnsc.FeatureClasses = dict;
        //        group.Add(tnsc);
        //        tnsc.Visible = true;
        //    }
        //    group.CollapseAll();
        //}
        #endregion
    }
        #endregion
}