using System;
using System.Windows.Forms;
namespace DFWinForms.LogicTree
{
    public delegate void OnHitTestHandler<T>(MouseButtons button, int x, int y, T layer);

}
