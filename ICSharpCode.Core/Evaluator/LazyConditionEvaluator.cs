using System;
namespace ICSharpCode.Core
{
    public class LazyConditionEvaluator : IConditionEvaluator
    {
        private AddIn addIn;
        private string name;
        private string className;
        public string Name
        {
            get
            {
                return this.name;
            }
        }
        public string ClassName
        {
            get
            {
                return this.className;
            }
        }
        public LazyConditionEvaluator(AddIn addIn, Properties properties)
        {
            this.addIn = addIn;
            this.name = properties["name"];
            this.className = properties["class"];
        }
        public bool IsValid(object caller, Condition condition)
        {
            IConditionEvaluator conditionEvaluator = (IConditionEvaluator)this.addIn.CreateObject(this.className);
            if (conditionEvaluator == null)
            {
                return false;
            }
            AddInTree.ConditionEvaluators[this.name] = conditionEvaluator;
            return conditionEvaluator.IsValid(caller, condition);
        }
        public override string ToString()
        {
            return string.Format("[LazyLoadConditionEvaluator: className = {0}, name = {1}]", this.className, this.name);
        }
    }
}
