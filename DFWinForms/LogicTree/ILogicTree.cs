using DevExpress.XtraTreeList;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
namespace DFWinForms.LogicTree
{
    public interface ILogicTree<T> : IPopMenu where T : IBaseLayer
    {
        event OnHitTestHandler<T> OnHitTest;
        event OnLayerDoubleClickHandler<T> OnLayerDoubleClick;
        event OnLayerEditHandler<T> OnLayerEdit;
        event OnRenamedHandler OnRenamed;
        string Caption
        {
            get;
            set;
        }
        bool ShowLayerControlBar
        {
            get;
            set;
        }
        bool ShowQuickSearchBar
        {
            get;
            set;
        }
        bool ShowRootLine
        {
            get;
            set;
        }
        bool ShowColorColumn
        {
            get;
            set;
        }
        bool ShowCustomColumn
        {
            get;
            set;
        }
        bool ShowCheckBoxes
        {
            get;
            set;
        }
        string CustomColumnCaption
        {
            get;
            set;
        }
        bool ShowColumnHeader
        {
            get;
            set;
        }
        bool AllowDragLayer
        {
            get;
            set;
        }
        bool AllowMultiSelect
        {
            get;
            set;
        }
        System.Windows.Forms.IWin32Window Owner
        {
            get;
        }
        TreeList TreeList
        {
            get;
        }
        void LayerEdit(T layer, EditType editType);
        void CreateRootLayer(string layerName);
        void ExportXML(string filepath);
        void ImportXML(string filepath);
        void ClearLayers();
        bool HitTest(int x, int y, ref T layer);
        System.Collections.Generic.List<T> GetSelectedLayers();
        T GetActiveLayer();
        System.Collections.Generic.List<T> GetRootLayers();
        void Refresh();
        System.Collections.Generic.List<T> SearchLayers(string blurValue, bool bAddToCmb);
        System.Collections.Generic.Dictionary<System.Type, System.Collections.Generic.List<string>> GetAllMenuItems(out System.Type type);
    }
}
