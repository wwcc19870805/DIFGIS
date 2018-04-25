using System;

namespace ICSharpCode.Core
{
    public class CompareConditionEvaluator : IConditionEvaluator
    {
        // Methods
        public bool IsValid(object caller, Condition condition)
        {
            StringComparison ordinalIgnoreCase;
            string str = condition.Properties["comparisonType"];
            if (string.IsNullOrEmpty(str))
            {
                ordinalIgnoreCase = StringComparison.OrdinalIgnoreCase;
            }
            else
            {
                ordinalIgnoreCase = (StringComparison)Enum.Parse(typeof(StringComparison), str);
            }
            return string.Equals(StringParser.Parse(condition.Properties["string"]), StringParser.Parse(condition.Properties["equals"]), ordinalIgnoreCase);
        }
    }


}
