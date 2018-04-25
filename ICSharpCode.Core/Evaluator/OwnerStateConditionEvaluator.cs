using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICSharpCode.Core
{
    public class OwnerStateConditionEvaluator : IConditionEvaluator
    {
        // Methods
        public bool IsValid(object caller, Condition condition)
        {
            if (caller is IOwnerState)
            {
                try
                {
                    Enum internalState = ((IOwnerState)caller).InternalState;
                    Enum enum3 = (Enum)Enum.Parse(internalState.GetType(), condition.Properties["ownerstate"]);
                    int num = int.Parse(internalState.ToString("D"));
                    int num2 = int.Parse(enum3.ToString("D"));
                    return ((num & num2) > 0);
                }
                catch (Exception)
                {
                    throw new ApplicationException("can't parse '" + condition.Properties["state"] + "'. Not a valid value.");
                }
            }
            return false;
        }
    }

 


}
