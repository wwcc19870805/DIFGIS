using System;
using System.Windows.Forms;
namespace ICSharpCode.Core
{
    public interface IDockPanelContent : System.IDisposable
    {
        Control Control
        {
            get;
        }
        void RedrawContent();
    }
}
