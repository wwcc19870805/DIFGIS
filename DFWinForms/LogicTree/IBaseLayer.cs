using DevExpress.XtraTreeList.Nodes;
using System;
using System.Drawing;
using System.Windows.Forms;
namespace DFWinForms.LogicTree
{
    public interface IBaseLayer :  IPopMenu
    {
        // Methods
        void Activate();
        ToolStripMenuItem AddMenuItem(string caption);
        ToolStripMenuItem AddMenuItem(string caption, Image image);
        ToolStripMenuItem AddMenuItem(string caption, int index);
        ToolStripMenuItem AddMenuItem(string parentCaption, string[] subCaptions);
        ToolStripMenuItem AddMenuItem(string caption, int index, Image image);
        ToolStripMenuItem AddMenuItem(string parentCaption, int index, string[] subCaptions);
        void AddMenuItems(string[] captions);
        void OnMenuItemClick(string caption);
        void OnMenuItemClick(ToolStripMenuItem item);
        void Release();
        void SelectAll();
        void SetLayerIndex(int index);

        // Properties
        CheckState CheckState { get; set; }
        object CustomValue { get; set; }
        string Details { get; set; }
        Point HitPoint { get; set; }
        string ID { get; }
        int ImageIndex { get; set; }
        string Info { get; set; }
        bool IsAuthFiltrated { get; set; }
        bool IsMutexLayer { get; }
        bool IsValid { get; set; }
        ILogicTree<IBaseLayer> LogicTree { get; set; }
        string Name { get; set; }
        int Opacity { get; set; }
        TreeListNode OwnNode { get; set; }
        IGroupLayer Parent { get; set; }
        object Tag { get; set; }
        bool Visible { get; set; }
    }
}
