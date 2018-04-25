using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gvitech.CityMaker.RenderControl;
using System.Xml;
using System.IO;
using DF3DData.Tree;
using Gvitech.CityMaker.FdeCore;

namespace DF3DData.Class
{
    public class Render3D
    {
        private static Render3D instance = null;
        private static readonly object syncRoot = new object();
        public static Render3D Instance
        {
            get
            {
                if (Render3D.instance == null)
                {
                    lock (syncRoot)
                    {
                        if (Render3D.instance == null)
                        {
                            Render3D.instance = new Render3D();
                        }
                    }
                }
                return Render3D.instance;
            }
        }
        private string renderPath;
        private Render3D()
        {
            this.renderPath = System.Windows.Forms.Application.StartupPath + @"\..\Resource\RenderXml\";
        }

        public void SetRender(TreeNodeFeatureClass treeNode, DF3DFeatureClass dffc)
        {
            if (treeNode == null || dffc == null) return;
            try
            {
                IFeatureClass fc = dffc.GetFeatureClass();
                if (fc == null) return;
                IFeatureLayer fl = dffc.GetFeatureLayer();
                if (fl == null) return;
                string name = string.IsNullOrEmpty(fc.AliasName) ? fc.Name : fc.AliasName;
                string xmlFileName = renderPath + name + ".xml";
                if (File.Exists(xmlFileName))
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(xmlFileName);
                    if (xmlDoc != null)
                    {
                        XmlNode featureLayerNode = xmlDoc.SelectSingleNode("FeatureLayer");
                        if (featureLayerNode == null) return;
                        XmlNode geometryRenderNode = featureLayerNode.SelectSingleNode("GeometryRender");
                        IGeometryRender geoRender = GetGeoRender(geometryRenderNode);
                        XmlNode textRenderNode = featureLayerNode.SelectSingleNode("TextRender");
                        ITextRender textRender = GetTextRender(textRenderNode);
                        fl.SetTextRender(textRender);
                        fl.SetGeometryRender(geoRender);
                        treeNode.Visible = false;
                    }
                }
            }
            catch (Exception ex) { }
        }

        private ITextRender GetTextRender(XmlNode node)
        {
            if (node == null)
            {
                return null;
            }
            ITextRender result = null;
            string value = node.Attributes["Expression"].Value;
            bool dynamicPlacement = false;
            bool minimizeOverlap = false;
            if (node.Attributes["DynamicPlacement"] != null)
            {
                dynamicPlacement = bool.Parse(node.Attributes["DynamicPlacement"].Value);
            }
            if (node.Attributes["MinimizeOverlap"] != null)
            {
                minimizeOverlap = bool.Parse(node.Attributes["MinimizeOverlap"].Value);
            }
            string value2 = node.Attributes["TextRenderType"].Value;
            if (value2 == gviRenderType.gviRenderSimple.ToString())
            {
                XmlNode xmlNode = node.SelectSingleNode("TextSymbol");
                if (xmlNode != null)
                {
                    result = new SimpleTextRenderClass
                    {
                        Symbol = this.GetTextSymbol(xmlNode),
                        Expression = value,
                        MinimizeOverlap = minimizeOverlap,
                        DynamicPlacement = dynamicPlacement
                    };
                }
            }
            else
            {
                XmlNodeList xmlNodeList = node.SelectNodes("ValueMap/TextScheme");
                if (xmlNodeList != null && xmlNodeList.Count > 0)
                {
                    IValueMapTextRender valueMapTextRender = new ValueMapTextRenderClass();
                    foreach (XmlNode xmlNode2 in xmlNodeList)
                    {
                        ITextRenderScheme textRenderScheme = new TextRenderSchemeClass();
                        XmlNode xmlNode3 = xmlNode2.SelectSingleNode("RenderRule");
                        if (xmlNode3 != null)
                        {
                            string value3 = xmlNode3.Attributes["RuleType"].Value;
                            string value4 = xmlNode3.Attributes["LookUpField"].Value;
                            if (value3 == gviRenderRuleType.gviRenderRuleUniqueValues.ToString())
                            {
                                IUniqueValuesRenderRule uniqueValuesRenderRule = new UniqueValuesRenderRuleClass();
                                string value5 = xmlNode3.Attributes["UniqueValue"].Value;
                                uniqueValuesRenderRule.LookUpField = value4;
                                uniqueValuesRenderRule.AddValue(value5);
                                textRenderScheme.AddRule(uniqueValuesRenderRule);
                            }
                            else
                            {
                                IRangeRenderRule rangeRenderRule = new RangeRenderRuleClass();
                                string value6 = xmlNode3.Attributes["IncludeMax"].Value;
                                string value7 = xmlNode3.Attributes["IncludeMin"].Value;
                                string value8 = xmlNode3.Attributes["MaxValue"].Value;
                                string value9 = xmlNode3.Attributes["MinValue"].Value;
                                rangeRenderRule.LookUpField = value4;
                                rangeRenderRule.IncludeMax = (value6.ToLower() == "true");
                                rangeRenderRule.IncludeMin = (value7.ToLower() == "true");
                                rangeRenderRule.MaxValue = double.Parse(value8);
                                rangeRenderRule.MinValue = double.Parse(value9);
                                textRenderScheme.AddRule(rangeRenderRule);
                            }
                        }
                        XmlNode xmlNode4 = xmlNode2.SelectSingleNode("TextSymbol");
                        if (xmlNode4 != null)
                        {
                            textRenderScheme.Symbol = this.GetTextSymbol(xmlNode4);
                        }
                        valueMapTextRender.AddScheme(textRenderScheme);
                    }
                    valueMapTextRender.Expression = value;
                    valueMapTextRender.MinimizeOverlap = minimizeOverlap;
                    valueMapTextRender.DynamicPlacement = dynamicPlacement;
                    result = valueMapTextRender;
                }
            }
            return result;
        }

        private ITextSymbol GetTextSymbol(XmlNode symbolNode)
        {
            if (symbolNode == null)
            {
                return null;
            }
            string value = symbolNode.Attributes["DrawLine"].Value;
            string value2 = symbolNode.Attributes["MaxVisualDistance"].Value;
            string value3 = symbolNode.Attributes["MinVisualDistance"].Value;
            string value4 = symbolNode.Attributes["Priority"].Value;
            string s = "0";
            string s2 = "0";
            if (symbolNode.Attributes["PivotOffsetX"] != null)
            {
                s = symbolNode.Attributes["PivotOffsetX"].Value;
                s2 = symbolNode.Attributes["PivotOffsetY"].Value;
            }
            string value5 = symbolNode.Attributes["PivotAlignment"].Value;
            string value6 = symbolNode.Attributes["VerticalOffset"].Value;
            ITextSymbol textSymbol = new TextSymbolClass();
            textSymbol.DrawLine = (value.ToLower() == "true");
            textSymbol.MaxVisualDistance = double.Parse(value2);
            textSymbol.MinVisualDistance = double.Parse(value3);
            textSymbol.MarginWidth = int.Parse(s);
            textSymbol.MarginHeight = int.Parse(s2);
            textSymbol.Priority = int.Parse(value4);
            textSymbol.PivotAlignment = (gviPivotAlignment)System.Enum.Parse(typeof(gviPivotAlignment), value5);
            textSymbol.VerticalOffset = double.Parse(value6);
            XmlNode xmlNode = symbolNode.SelectSingleNode("TextAttribute");
            if (xmlNode != null)
            {
                string value7 = xmlNode.Attributes["TextColor"].Value;
                string value8 = xmlNode.Attributes["Font"].Value;
                string value9 = xmlNode.Attributes["TextSize"].Value;
                string value10 = xmlNode.Attributes["BackgroundColor"].Value;
                string value11 = xmlNode.Attributes["OutlineColor"].Value;
                string value12 = xmlNode.Attributes["Bold"].Value;
                string value13 = xmlNode.Attributes["Italic"].Value;
                string value14 = xmlNode.Attributes["Underline"].Value;
                textSymbol.TextAttribute = new TextAttributeClass
                {
                    BackgroundColor = uint.Parse(value10),
                    TextColor = uint.Parse(value7),
                    Font = value8,
                    OutlineColor = uint.Parse(value11),
                    TextSize = int.Parse(value9),
                    Bold = bool.Parse(value12),
                    Italic = bool.Parse(value13),
                    Underline = bool.Parse(value14)
                };
            }
            return textSymbol;
        }

        private IGeometrySymbol GetGeometrySymbol(string GeoType, XmlNode symbolNode)
        {
            IGeometrySymbol result = null;
            if (GeoType != null)
            {
                if (!(GeoType == "ModelPoint"))
                {
                    if (!(GeoType == "PointCloud"))
                    {
                        if (!(GeoType == "SimplePoint"))
                        {
                            if (!(GeoType == "ImagePoint"))
                            {
                                if (!(GeoType == "Polyline"))
                                {
                                    if (GeoType == "Polygon")
                                    {
                                        result = new SurfaceSymbolClass
                                        {
                                            Color = uint.Parse(symbolNode.Attributes["FillColor"].Value),
                                            EnableLight = bool.Parse(symbolNode.Attributes["EnableLight"].Value),
                                            BoundarySymbol =
                                            {
                                                Color = uint.Parse(symbolNode.Attributes["Color"].Value),
                                                Width = float.Parse(symbolNode.Attributes["Width"].Value),
                                                ImageName = symbolNode.Attributes["ImageName"].Value
                                            }
                                        };
                                    }
                                }
                                else
                                {
                                    ICurveSymbol curveSymbol = new CurveSymbolClass();
                                    curveSymbol.Color = uint.Parse(symbolNode.Attributes["Color"].Value);
                                    curveSymbol.Width = float.Parse(symbolNode.Attributes["Width"].Value);
                                    curveSymbol.ImageName = symbolNode.Attributes["ImageName"].Value;
                                    if (symbolNode.Attributes["RepeatLength"] != null)
                                    {
                                        curveSymbol.RepeatLength = float.Parse(symbolNode.Attributes["RepeatLength"].Value);
                                    }
                                    result = curveSymbol;
                                }
                            }
                            else
                            {
                                result = new ImagePointSymbolClass
                                {
                                    Size = int.Parse(symbolNode.Attributes["Size"].Value),
                                    Alignment = (gviPivotAlignment)System.Enum.Parse(typeof(gviPivotAlignment), symbolNode.Attributes["Alignment"].Value),
                                    ImageName = Path.Combine(System.Windows.Forms.Application.StartupPath + @"\..\Image\" + symbolNode.Attributes["ImageName"].Value)
                                };
                            }
                        }
                        else
                        {
                            result = new SimplePointSymbolClass
                            {
                                Size = int.Parse(symbolNode.Attributes["Size"].Value),
                                Alignment = (gviPivotAlignment)System.Enum.Parse(typeof(gviPivotAlignment), symbolNode.Attributes["Alignment"].Value),
                                FillColor = uint.Parse(symbolNode.Attributes["FillColor"].Value),
                                Style = (gviSimplePointStyle)System.Enum.Parse(typeof(gviSimplePointStyle), symbolNode.Attributes["Style"].Value)
                            };
                        }
                    }
                    else
                    {
                        result = new PointCloudSymbolClass
                        {
                            Color = uint.Parse(symbolNode.Attributes["Color"].Value),
                            EnableColor = symbolNode.Attributes["EnableColor"].Value.ToLower() == "true",
                            Size = int.Parse(symbolNode.Attributes["Size"].Value)
                        };
                    }
                }
                else
                {
                    IModelPointSymbol modelPointSymbol = new ModelPointSymbolClass();
                    modelPointSymbol.Color = uint.Parse(symbolNode.Attributes["Color"].Value);
                    modelPointSymbol.EnableColor = (symbolNode.Attributes["EnableColor"].Value.ToLower() == "true");
                    XmlAttribute xmlAttribute = symbolNode.Attributes["EnableTexture"];
                    if (xmlAttribute != null)
                    {
                        modelPointSymbol.EnableTexture = (xmlAttribute.Value.ToLower() == "true");
                    }
                    result = modelPointSymbol;
                }
            }
            return result;
        }

        private IGeometryRender GetGeoRender(XmlNode node)
        {
            if (node == null)
            {
                return null;
            }
            IGeometryRender result = null;
            double heightOffset = 0.0;
            string value = node.Attributes["HeightStyle"].Value;
            object obj = node.Attributes["HeightOffset"];
            if (obj != null)
            {
                heightOffset = double.Parse(node.Attributes["HeightOffset"].Value);
            }
            string value2 = node.Attributes["GroupField"].Value;
            string value3 = node.Attributes["RenderType"].Value;
            if (value3 == gviRenderType.gviRenderSimple.ToString())
            {
                ISimpleGeometryRender simpleGeometryRender = new SimpleGeometryRenderClass();
                XmlNode xmlNode = node.SelectSingleNode("GeometrySymbol");
                if (xmlNode != null)
                {
                    string value4 = xmlNode.Attributes["GeometryType"].Value;
                    simpleGeometryRender.Symbol = this.GetGeometrySymbol(value4, xmlNode);
                }
                if (value == gviHeightStyle.gviHeightAbsolute.ToString())
                {
                    simpleGeometryRender.HeightStyle = gviHeightStyle.gviHeightAbsolute;
                }
                else
                {
                    simpleGeometryRender.HeightStyle = gviHeightStyle.gviHeightOnTerrain;
                }
                simpleGeometryRender.HeightOffset = heightOffset;
                simpleGeometryRender.RenderGroupField = value2;
                result = simpleGeometryRender;
            }
            else
            {
                XmlNodeList xmlNodeList = node.SelectNodes("ValueMap/RenderScheme");
                if (xmlNodeList != null && xmlNodeList.Count > 0)
                {
                    IValueMapGeometryRender valueMapGeometryRender = new ValueMapGeometryRenderClass();
                    foreach (XmlNode xmlNode2 in xmlNodeList)
                    {
                        IGeometryRenderScheme geometryRenderScheme = new GeometryRenderSchemeClass();
                        XmlNode xmlNode3 = xmlNode2.SelectSingleNode("RenderRule");
                        if (xmlNode3 != null)
                        {
                            string value5 = xmlNode3.Attributes["RuleType"].Value;
                            string value6 = xmlNode3.Attributes["LookUpField"].Value;
                            if (value5 == gviRenderRuleType.gviRenderRuleUniqueValues.ToString())
                            {
                                IUniqueValuesRenderRule uniqueValuesRenderRule = new UniqueValuesRenderRuleClass();
                                uniqueValuesRenderRule.LookUpField = value6;
                                if (xmlNode3.Attributes["UniqueValue"] != null)
                                {
                                    string value7 = xmlNode3.Attributes["UniqueValue"].Value;
                                    uniqueValuesRenderRule.AddValue(value7);
                                }
                                geometryRenderScheme.AddRule(uniqueValuesRenderRule);
                            }
                            else
                            {
                                IRangeRenderRule rangeRenderRule = new RangeRenderRuleClass();
                                string value8 = xmlNode3.Attributes["IncludeMax"].Value;
                                string value9 = xmlNode3.Attributes["IncludeMin"].Value;
                                string value10 = xmlNode3.Attributes["MaxValue"].Value;
                                string value11 = xmlNode3.Attributes["MinValue"].Value;
                                rangeRenderRule.LookUpField = value6;
                                rangeRenderRule.IncludeMax = (value8.ToLower() == "true");
                                rangeRenderRule.IncludeMin = (value9.ToLower() == "true");
                                rangeRenderRule.MaxValue = double.Parse(value10);
                                rangeRenderRule.MinValue = double.Parse(value11);
                                geometryRenderScheme.AddRule(rangeRenderRule);
                            }
                        }
                        XmlNode xmlNode4 = xmlNode2.SelectSingleNode("GeometrySymbol");
                        if (xmlNode4 != null)
                        {
                            string value12 = xmlNode4.Attributes["GeometryType"].Value;
                            IGeometrySymbol geometrySymbol = this.GetGeometrySymbol(value12, xmlNode4);
                            geometryRenderScheme.Symbol = geometrySymbol;
                        }
                        if (value == gviHeightStyle.gviHeightAbsolute.ToString())
                        {
                            valueMapGeometryRender.HeightStyle = gviHeightStyle.gviHeightAbsolute;
                        }
                        else
                        {
                            valueMapGeometryRender.HeightStyle = gviHeightStyle.gviHeightOnTerrain;
                        }
                        valueMapGeometryRender.HeightOffset = heightOffset;
                        valueMapGeometryRender.RenderGroupField = value2;
                        valueMapGeometryRender.AddScheme(geometryRenderScheme);
                    }
                    result = valueMapGeometryRender;
                }
            }
            return result;
        }
    }
}
