using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DFWinForms.UserControl
{
    public interface IPadContent : IContent
    {
        string Pos { get; set; }
        bool AutoHide { get; set; }
        int PHeight { get; set; }
    }
}
