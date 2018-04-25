using System;
namespace ICSharpCode.Core
{
    public interface IConditionEvaluator
    {
        bool IsValid(object caller, Condition condition);
    }
}
