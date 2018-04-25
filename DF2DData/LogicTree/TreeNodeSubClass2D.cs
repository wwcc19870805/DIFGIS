using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DFWinForms.LogicTree;
using ESRI.ArcGIS.Carto;
using DFDataConfig.Logic;
using ESRI.ArcGIS.Geodatabase;
using DF2DData.Class;
using DF2DControl.Base;

namespace DF2DData.LogicTree
{
    public  class TreeNodeSubClass2D : BaseLayerClass
    {
        private Dictionary<string, DF2DFeatureClass> dictFCs;
        private Dictionary<string, IFeatureLayer> dictFLs;//当前二级子类所分布在的图层
        private bool _checkOn;
        string _classifyField;

        public bool CheckOn
        {
            get { return this._checkOn; }
            set { this._checkOn = value; }
        }
       
        public Dictionary<string, IFeatureLayer> DictFLs
        {
            get { return this.dictFLs; }
            set { this.dictFLs = value; }
        }
       
        public string ClassifyField
        {
            get { return this._classifyField; }
            set { this._classifyField = value; }
        }
        public Dictionary<string, DF2DFeatureClass> FeatureClasses
        {
            get { return this.dictFCs; }
            set { this.dictFCs = value; }
        }
        public TreeNodeSubClass2D()
            : this("")
        {
            base.ImageIndex = 3;
        }
        public TreeNodeSubClass2D(string ID)
            : base(ID)
        {
        }
        public override bool Visible
        {
            get
            {
                return base.Visible;
            }
            set
            {

                if (this.CustomValue != null)
                {
                    if (this.dictFLs == null || this.CustomValue == null) return;
                    if (this.CustomValue is SubClass)//如果CustomValue是二级子类
                    {
                        (this.CustomValue as SubClass).Visible2D = value;
                    }
                    if (value)//如果是显示
                    {
                        string whereclause = this.ClassifyField + "=" + "'" + this.Name + "'";
                        List<IBaseLayer> parentSC = this.Parent.SelectAllSubLayers();//选择当前二级子类父类下的所有子类
                        if (parentSC == null) whereclause = this.ClassifyField + "=" + "'" + this.Name + "'";
                        if (parentSC.Count > 0)
                        {
                            foreach (IBaseLayer lyr in parentSC)//对所有二级子类进行遍历
                            {
                                if (lyr.Visible == true)//如果当前二级子类为显示
                                    whereclause = whereclause + "OR" + " " + this.ClassifyField + "=" + "'" + lyr.Name + "'";
                                //更新where语句，增加当前二级子类为显示项
                            }
                        }

                        foreach (IFeatureLayer ftl in dictFLs.Values)//对当前子类对应的图层进行遍历  dicFls.Values有问题，查配置文件
                        {
                            IFeatureLayerDefinition pDef = ftl as IFeatureLayerDefinition;
                            pDef.DefinitionExpression = whereclause;//筛选出需要显示的要素
                        }

                        base.Visible = true;
                        DF2DApplication app = DF2DApplication.Application;
                        if (app == null) return;
                        app.Current2DMapControl.ActiveView.Refresh();

                    }
                    else
                    {
                        string whereclause = "";
                        base.Visible = false;
                        List<IBaseLayer> parentSC = this.Parent.SelectAllSubLayers();//选择当前二级子类父类下的所有子类
                        if (parentSC == null) whereclause = "";
                        if (parentSC.Count > 0)
                        {
                            foreach (IBaseLayer lyr in parentSC)//对所有二级子类进行遍历
                            {
                                if (lyr.Visible == true)//如果当前二级子类为显示
                                    whereclause = whereclause + " OR " + this.ClassifyField + "= '" + lyr.Name + "'";
                                //更新where语句，增加当前二级子类为显示项
                            }
                        }

                        foreach (IFeatureLayer ftl in dictFLs.Values)
                        {
                            IFeatureLayerDefinition pDef = ftl as IFeatureLayerDefinition;
                            if (whereclause != "") pDef.DefinitionExpression = whereclause;
                            else pDef.DefinitionExpression = "1 <> 1";
                        }

                        //base.Visible = false;
                        DF2DApplication app = DF2DApplication.Application;
                        if (app == null) return;
                        app.Current2DMapControl.ActiveView.Refresh();

                    }
                }
                else
                {
                    base.Visible = false;
                    
                } 
            }
        }
    }
}
