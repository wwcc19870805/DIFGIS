using System;
namespace ICSharpCode.Core
{
    public enum AddInAction
    {
        Enable,
        Disable,
        Install,
        Uninstall,
        Update,
        InstalledTwice,
        DependencyError,
        CustomError
    }
}
