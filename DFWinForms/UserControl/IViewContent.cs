using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICSharpCode.Core;

namespace DFWinForms.UserControl
{
    public interface IViewContent : IContent
    {
        bool Bind(ICommand cmd);
        void UnBind(ICommand cmd);
        
    }
}
