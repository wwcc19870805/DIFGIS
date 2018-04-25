using System;
namespace ICSharpCode.Core
{
    public interface ICommand
    {
        event StatusUpdateHandle StatusChanged;
        bool HighLight
        {
            get;
            set;
        }
        string CommandName
        {
            get;
            set;
        }
        string CommandID
        {
            get;
            set;
        }
        object Hook
        {
            get;

            set;
        }
        void Init(object sender);
        void Run(object sender, System.EventArgs e);
        void RestoreEnv();
        bool CheckPermission();
    }
}
