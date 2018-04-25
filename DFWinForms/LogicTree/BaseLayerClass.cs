using DevExpress.XtraTreeList.Nodes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
namespace DFWinForms.LogicTree
{
    public class BaseLayerClass : IBaseLayer,  IPopMenu
    {
        // Fields
        private object _customValue;
        protected string _describe;
        private string _details;
        protected readonly string _guid;
        private int _imageIndex;
        private string _info;
        private bool _isAuthFiltrated;
        protected bool _isMutexLayer;
        protected bool _isValid;
        private string _layername;
        private int _opaCity;
        protected TreeListNode _ownNode;
        private IGroupLayer _parent;
        protected ContextMenuStrip _popMenu;
        private bool _showPopMenu;
        private object _tag;
        private bool _visible;
        protected const string ConStrName = "Name";
        private object lockOb;

        // Methods
        public BaseLayerClass()
            : this("")
        {
        }

        public BaseLayerClass(string layerID)
        {
            this._isValid = true;
            this._guid = "";
            this._layername = "";
            this._showPopMenu = true;
            this._details = "";
            this._opaCity = 0xff;
            this.lockOb = new object();
            this._info = "";
            this._guid = string.IsNullOrEmpty(layerID) ? Guid.NewGuid().ToString() : layerID;
            this._popMenu = new ContextMenuStrip();
            this.InitPopMenu();
        }

        public void Activate()
        {
            if (this.OwnNode != null)
            {
                this.OwnNode.TreeList.FocusedNode = this.OwnNode;
            }
        }

        public ToolStripMenuItem AddMenuItem(string caption)
        {
            if (string.IsNullOrEmpty(caption.Trim()))
            {
                this._popMenu.Items.Add(new ToolStripSeparator());
                return null;
            }
            return (this._popMenu.Items.Add(caption, null, new EventHandler(this.OnMenuItemClick)) as ToolStripMenuItem);
        }

        public ToolStripMenuItem AddMenuItem(string parentCaption, string[] subCaptions)
        {
            if ((string.IsNullOrEmpty(parentCaption.Trim()) || (subCaptions == null)) || (subCaptions.Length == 0))
            {
                return null;
            }
            ToolStripMenuItem item = this._popMenu.Items.Add(parentCaption) as ToolStripMenuItem;
            if (item != null)
            {
                foreach (string str in subCaptions)
                {
                    if (string.IsNullOrEmpty(str.Trim()))
                    {
                        item.DropDownItems.Add(new ToolStripSeparator());
                    }
                    else
                    {
                        item.DropDownItems.Add(str, null, new EventHandler(this.OnMenuItemClick));
                    }
                }
            }
            return item;
        }

        public ToolStripMenuItem AddMenuItem(string caption, Image image)
        {
            if (string.IsNullOrEmpty(caption.Trim()))
            {
                this._popMenu.Items.Add(new ToolStripSeparator());
                return null;
            }
            return (this._popMenu.Items.Add(caption, image, new EventHandler(this.OnMenuItemClick)) as ToolStripMenuItem);
        }

        public ToolStripMenuItem AddMenuItem(string caption, int index)
        {
            if ((index <= -1) || (index >= this._popMenu.Items.Count))
            {
                return this.AddMenuItem(caption);
            }
            if (string.IsNullOrEmpty(caption.Trim()))
            {
                this._popMenu.Items.Insert(index, new ToolStripSeparator());
                return null;
            }
            ToolStripMenuItem item = new ToolStripMenuItem(caption, null, new EventHandler(this.OnMenuItemClick));
            this._popMenu.Items.Insert(index, item);
            return item;
        }

        public ToolStripMenuItem AddMenuItem(string caption, int index, Image image)
        {
            if ((index <= -1) || (index >= this._popMenu.Items.Count))
            {
                return this.AddMenuItem(caption, image);
            }
            if (string.IsNullOrEmpty(caption.Trim()))
            {
                this._popMenu.Items.Insert(index, new ToolStripSeparator());
                return null;
            }
            ToolStripMenuItem item = new ToolStripMenuItem(caption, image, new EventHandler(this.OnMenuItemClick));
            this._popMenu.Items.Insert(index, item);
            return item;
        }

        public ToolStripMenuItem AddMenuItem(string parentCaption, int index, string[] subCaptions)
        {
            if ((string.IsNullOrEmpty(parentCaption.Trim()) || (subCaptions == null)) || (subCaptions.Length == 0))
            {
                return null;
            }
            if ((index <= -1) || (index >= this._popMenu.Items.Count))
            {
                return this.AddMenuItem(parentCaption, subCaptions);
            }
            ToolStripMenuItem item = new ToolStripMenuItem(parentCaption);
            if (item != null)
            {
                this._popMenu.Items.Insert(index, item);
                foreach (string str in subCaptions)
                {
                    if (string.IsNullOrEmpty(str.Trim()))
                    {
                        item.DropDownItems.Add(new ToolStripSeparator());
                    }
                    else
                    {
                        item.DropDownItems.Add(str, null, new EventHandler(this.OnMenuItemClick));
                    }
                }
            }
            return item;
        }

        public void AddMenuItems(string[] captions)
        {
            if ((captions != null) && (captions.Length > 0))
            {
                foreach (string str in captions)
                {
                    this.AddMenuItem(str);
                }
            }
        }

        public List<string> GetMenuItems(out Type type)
        {
            type = base.GetType();
            List<string> list = new List<string>();
            foreach (ToolStripItem item in this.PopMenu.Items)
            {
                if (!(item is ToolStripSeparator) && !list.Contains(item.Text))
                {
                    list.Add(item.Text);
                }
            }
            return list;
        }

        public virtual void InitPopMenu()
        {
        }

        public virtual void OnMenuItemClick(string caption)
        {
        }

        public virtual void OnMenuItemClick(ToolStripMenuItem item)
        {
        }

        public void OnMenuItemClick(object sender, EventArgs e)
        {
            Application.DoEvents();
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            if (item != null)
            {
                if ((item.Tag == null) || (item.Tag.ToString() == string.Empty))
                {
                    this.OnMenuItemClick(item.Text);
                }
                else
                {
                    this.OnMenuItemClick(item.Tag.ToString());
                }
                this.OnMenuItemClick(item);
            }
        }

        public virtual void Release()
        {
            if (this.OwnNode.ParentNode != null)
            {
                if (this.Parent != null)
                {
                    this.Parent.Delete(this);
                }
            }
            else if (this.OwnNode.TreeList != null)
            {
                this.OwnNode.TreeList.Nodes.Remove(this.OwnNode);
            }
            if (this.LogicTree != null)
            {
                this.LogicTree.ExportXML(null);
            }
        }

        public void Rename()
        {
            if (this._ownNode.TreeList.FocusedNode != null)
            {
                this._ownNode.TreeList.FocusedNode.TreeList.Columns.ColumnByFieldName("Name").OptionsColumn.AllowEdit = true;
                this._ownNode.TreeList.FocusedNode.TreeList.Columns.ColumnByFieldName("Name").OptionsColumn.AllowFocus = true;
                this._ownNode.TreeList.FocusedColumn = this._ownNode.TreeList.FocusedNode.TreeList.Columns.ColumnByFieldName("Name");
                this._ownNode.TreeList.ShowEditor();
            }
        }

        public virtual void SelectAll()
        {
        }

        private void SetMutex()
        {
            lock (this.lockOb)
            {
                if ((this._isMutexLayer && this._visible) && (this.Parent != null))
                {
                    IBaseLayer layerByIndex = null;
                    int layerCount = this.Parent.GetLayerCount();
                    for (int i = 0; i < layerCount; i++)
                    {
                        layerByIndex = this.Parent.GetLayerByIndex(i);
                        if ((!this.ID.Equals(layerByIndex.ID) && layerByIndex.IsMutexLayer) && layerByIndex.Visible)
                        {
                            layerByIndex.Visible = false;
                        }
                    }
                }
            }
        }

        public void SetLayerIndex(int index)
        {
            if (this.OwnNode != null)
            {
                int count = 0;
                if (this.OwnNode.ParentNode == null)
                {
                    count = this.OwnNode.TreeList.Nodes.Count;
                }
                else
                {
                    count = this.OwnNode.ParentNode.Nodes.Count;
                }
                if ((index < count) && (index > -1))
                {
                    this.OwnNode.TreeList.SetNodeIndex(this.OwnNode, index);
                }
                else if (index == -1)
                {
                    this.OwnNode.TreeList.SetNodeIndex(this.OwnNode, count - 1);
                }
            }
        }

        public override string ToString()
        {
            return this.Name;
        }

        // Properties

        public virtual CheckState CheckState
        {
            get
            {
                if (this._ownNode != null)
                {
                    return this._ownNode.CheckState;
                }
                return CheckState.Unchecked;
            }
            set
            {
                if (this._ownNode != null)
                {
                    if (value == CheckState.Checked)
                    {
                        this.Visible = true;
                    }
                    else if (value == CheckState.Unchecked)
                    {
                        this.Visible = false;
                    }
                    else
                    {
                        this._ownNode.CheckState = value;
                    }
                }
            }
        }

        public object CustomValue
        {
            get
            {
                return this._customValue;
            }
            set
            {
                this._customValue = value;
                if (this._ownNode != null)
                {
                    this._ownNode.SetValue("Custom", this._customValue);
                }
            }
        }

        public virtual string Details
        {
            get
            {
                return this._details;
            }
            set
            {
                this._details = value;
            }
        }

        public Point HitPoint { get; set; }

        public string ID
        {
            get
            {
                return this._guid;
            }
        }

        public int ImageIndex
        {
            get
            {
                return this._imageIndex;
            }
            set
            {
                this._imageIndex = value;
                if (this.OwnNode != null)
                {
                    this.OwnNode.StateImageIndex = this._imageIndex;
                }
            }
        }

        public string Info
        {
            get
            {
                return this._info;
            }
            set
            {
                this._info = value;
            }
        }

        public bool IsAuthFiltrated
        {
            get
            {
                return this._isAuthFiltrated;
            }
            set
            {
                this._isAuthFiltrated = value;
            }
        }

        public bool IsMutexLayer
        {
            get
            {
                return this._isMutexLayer;
            }
        }

        public bool IsValid
        {
            get
            {
                return this._isValid;
            }
            set
            {
                this._isValid = value;
            }
        }

        public virtual string LayerDescribe
        {
            get
            {
                return this._describe;
            }
            set
            {
                this._describe = value;
            }
        }

        public ILogicTree<IBaseLayer> LogicTree { get; set; }

        public virtual string Name
        {
            get
            {
                return this._layername;
            }
            set
            {
                this._layername = value;
                if (this._ownNode != null)
                {
                    this._ownNode.SetValue("Name", this._layername);
                }
            }
        }

        public virtual int Opacity
        {
            get
            {
                return this._opaCity;
            }
            set
            {
                this._opaCity = value;
            }
        }

        public TreeListNode OwnNode
        {
            get
            {
                return this._ownNode;
            }
            set
            {
                this._ownNode = value;
                if (this._ownNode != null)
                {
                    this._ownNode.Tag = this;
                    this._ownNode.StateImageIndex = this.ImageIndex;
                }
            }
        }

        public IGroupLayer Parent
        {
            get
            {
                return this._parent;
            }
            set
            {
                this._parent = value;
            }
        }

        public virtual ContextMenuStrip PopMenu
        {
            get
            {
                return this._popMenu;
            }
            set
            {
                this._popMenu = value;
            }
        }

        public bool ShowPopMenu
        {
            get
            {
                return this._showPopMenu;
            }
            set
            {
                this._showPopMenu = value;
            }
        }

        public object Tag
        {
            get
            {
                return this._tag;
            }
            set
            {
                this._tag = value;
            }
        }

        public virtual bool Visible
        {
            get
            {
                return this._visible;
            }
            set
            {
                this._visible = value;
                if (this._ownNode != null)
                {
                    this._ownNode.Checked = this._visible;
                }
                this.SetMutex();
            }
        }
    }
}
