using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using DevExpress.XtraEditors;
using ICSharpCode.Core;
using DevExpress.XtraTreeList.Nodes;

namespace DFWinForms.LogicTree
{
    public class GroupLayerClass : BaseLayerClass, IGroupLayer, IBaseLayer,  IPopMenu
    {
        protected List<IBaseLayer> _childLayer;
        protected Dictionary<string, IBaseLayer> _layerMap;

        public GroupLayerClass()
            : this("")
        {
        }

        public GroupLayerClass(string groupid)
            : base(groupid)
        {
            base.ImageIndex = 11;
            this._layerMap = new Dictionary<string, IBaseLayer>();
            this._childLayer = new List<IBaseLayer>();
        }

        public virtual void Add(IBaseLayer layer)
        {
            if (layer != null)
            {
                this.Add2(layer);
                if (base.LogicTree != null)
                {
                    base.LogicTree.ExportXML(null);
                }
            }
        }

        public void Add(IBaseLayer layer, int index)
        {
            if (layer != null)
            {
                if (this._layerMap.ContainsKey(layer.ID))
                {
                    XtraMessageBox.Show(StringParser.Parse("${res:TipText_该图层已经存在}"));
                }
                else
                {
                    TreeListNode node = base._ownNode.TreeList.AppendNode(new object[] { layer.Name }, base._ownNode);
                    if (node != null)
                    {
                        if (index < this.GetLayerCount())
                        {
                            base.OwnNode.TreeList.SetNodeIndex(node, index);
                        }
                        layer.OwnNode = node;
                        layer.Parent = this;
                        node.Checked = layer.Visible;
                        this._layerMap.Add(layer.ID, layer);
                        layer.LogicTree = base.LogicTree;
                        this.Expanded = false;
                    }
                    else
                    {
                        XtraMessageBox.Show("图层创建失败", "提示");
                        return;
                    }
                    layer.Activate();
                    this._childLayer.Add(layer);
                    if (base.LogicTree != null)
                    {
                        base.LogicTree.ExportXML(null);
                    }
                }
            }
        }

        public virtual void Add2(IBaseLayer layer)
        {
            if (layer != null)
            {
                if (this.ExistLayer(layer.ID))
                {
                    throw new Exception(StringParser.Parse("${res:TipText_该图层已存在}"));
                }
                TreeListNode node = base._ownNode.TreeList.AppendNode(new object[] { layer.Name, layer }, base._ownNode);
                if (node == null)
                {
                    throw new Exception("图层创建失败");
                }
                layer.OwnNode = node;
                layer.Parent = this;
                node.Checked = layer.Visible;
                this._layerMap.Add(layer.ID, layer);
                layer.LogicTree = base.LogicTree;
                layer.CustomValue = layer.CustomValue;
                layer.Activate();
                this._childLayer.Add(layer);
            }
        }

        public virtual void Clear()
        {
            List<IBaseLayer> list = this.SelectAllSubLayers();
            if ((list != null) && (list.Count > 0))
            {
                foreach (IBaseLayer layer in list)
                {
                    layer.Release();
                }
            }
            this._layerMap.Clear();
        }

        public virtual void CollapseAll()
        {
            if (base._ownNode != null)
            {
                base._ownNode.Expanded = false;
                base._ownNode.Selected = false;
            }
        }

        public virtual void Delete(IBaseLayer layer)
        {
            if (layer != null)
            {
                if (!this._layerMap.ContainsKey(layer.ID))
                {
                    XtraMessageBox.Show(StringParser.Parse("${res:TipText_不存在该图层}"));
                }
                else
                {
                    if ((base.OwnNode != null) && (layer.OwnNode != null))
                    {
                        base.OwnNode.Nodes.Remove(layer.OwnNode);
                        if (base.LogicTree != null)
                        { 
                            base.LogicTree.ExportXML(null);
                        }
                    }
                    this._layerMap.Remove(layer.ID);
                    this._childLayer.Remove(layer);
                }
            }
        }

        public bool ExistLayer(string guid)
        {
            return this._layerMap.ContainsKey(guid);
        }

        public bool ExistLayerName(string layername)
        {
            int layerCount = this.GetLayerCount();
            for (int i = 0; i < layerCount; i++)
            {
                if (layername.Equals(this.GetLayerByIndex(i).Name))
                {
                    return true;
                }
            }
            return false;
        }

        public virtual void ExpandAll()
        {
            if (base._ownNode != null)
            {
                base._ownNode.ExpandAll();
            }
        }

        public virtual string ExportToXML()
        {
            try
            {
                if (base.OwnNode.Nodes.Count > 0)
                {
                    XmlWriterSettings settings = new XmlWriterSettings
                    {
                        Indent = true
                    };
                    XmlWriter xw = null;
                    StringBuilder output = new StringBuilder();
                    xw = XmlWriter.Create(output, settings);
                    xw.WriteStartDocument();
                    xw.WriteStartElement(base.GetType().Name);
                    xw.WriteAttributeString("Name", this.Name);
                    xw.WriteAttributeString("Guid", base.ID);
                    for (int i = 0; i < base.OwnNode.Nodes.Count; i++)
                    {
                        this.WriteXML(base.OwnNode.Nodes[i], xw);
                    }
                    xw.WriteEndElement();
                    xw.Close();
                    return output.ToString();
                }
            }
            catch (Exception exception)
            {
               LoggingService.Error("导出XML:" + exception.Message);
            }
            return null;
        }

        public virtual IBaseLayer GetLayerById(string layerid)
        {
            if (!this._layerMap.ContainsKey(layerid))
            {
                XtraMessageBox.Show(StringParser.Parse("${res:TipText_不存在该图层"));
                return null;
            }
            return this._layerMap[layerid];
        }

        public IBaseLayer GetLayerByIndex(int index)
        {
            if (base.OwnNode.Nodes.Count > 0)
            {
                return (base.OwnNode.Nodes[index].Tag as IBaseLayer);
            }
            return null;
        }

        public virtual int GetLayerCount()
        {
            return this._layerMap.Count;
        }

        public virtual bool ImportXML(string xmlInfo)
        {
            return false;
        }

        public override void InitPopMenu()
        {
        }

        public override void OnMenuItemClick(string caption)
        {
        }

        public override void Release()
        {
            this.Clear();
            base.Release();
        }

        public List<IBaseLayer> SelectAllSubLayers()
        {
            return this._layerMap.Values.ToList<IBaseLayer>();
        }

        private void WriteXML(TreeListNode node, XmlWriter xw)
        {
            IBaseLayer tag = null;
            if (node.Tag != null)
            {
                tag = node.Tag as IBaseLayer;
                if (tag != null)
                {
                    xw.WriteStartElement(tag.GetType().Name);
                    xw.WriteAttributeString("Name", tag.Name);
                    xw.WriteAttributeString("Guid", tag.ID);
                    if (node.Nodes.Count > 0)
                    {
                        foreach (TreeListNode node2 in node.Nodes)
                        {
                            this.WriteXML(node2, xw);
                        }
                    }
                    xw.WriteEndElement();
                }
            }
        }

        public bool Expanded
        {
            get
            {
                if (base._ownNode != null)
                {
                    return base._ownNode.Expanded;
                }
                return true;
            }
            set
            {
                if (base._ownNode != null)
                {
                    base._ownNode.Expanded = value;
                }
            }
        }

        public override bool Visible
        {
            get
            {
                return base.Visible;
            }
            set
            {
                base.Visible = value;
                int layerCount = this.GetLayerCount();
                if (layerCount > 0)
                {
                    for (int i = 0; i < layerCount; i++)
                    {
                        IBaseLayer layerByIndex = this.GetLayerByIndex(i);
                        if ((layerByIndex != null) && (!layerByIndex.IsMutexLayer || !value))
                        {
                            layerByIndex.Visible = base.Visible;
                        }
                    }
                }
            }
        }

    }
}
