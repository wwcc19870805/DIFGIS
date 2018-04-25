using System;
using System.Collections.Generic;
using System.Windows.Forms;
namespace DFWinForms.LogicTree
{
    public interface IPopMenu
    {
        // Methods
        List<string> GetMenuItems(out Type type);
        void InitPopMenu();
        void OnMenuItemClick(object sender, EventArgs e);

        // Properties
        ContextMenuStrip PopMenu { get; set; }
        bool ShowPopMenu { get; set; }
    }
}
