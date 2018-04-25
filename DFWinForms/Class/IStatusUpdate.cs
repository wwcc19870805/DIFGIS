using System;
namespace DFWinForms.Class
{
    public interface IStatusUpdate
    {
        bool HighLight
        {
            get;
            set;
        }
        void UpdateStatus();
        void UpdateText();
    }
}
