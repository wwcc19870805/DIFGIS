using System;
namespace ICSharpCode.Core
{
    public interface ICondition
    {
        string Name
        {
            get;
        }
        ConditionAction Action
        {
            get;
            set;
        }
        bool IsValid(object caller);
    }
}
