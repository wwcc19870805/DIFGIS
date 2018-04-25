using System;
using System.Collections;
namespace ICSharpCode.Core
{
    public class Codon
    {
        private AddIn addIn;
        private string name;
        private Properties properties;
        private ICondition[] conditions;
        public string Name
        {
            get
            {
                return this.name;
            }
        }
        public AddIn AddIn
        {
            get
            {
                return this.addIn;
            }
        }
        public string Id
        {
            get
            {
                return this.properties["id"];
            }
        }
        public string InsertAfter
        {
            get
            {
                if (!this.properties.Contains("insertafter"))
                {
                    return "";
                }
                return this.properties["insertafter"];
            }
            set
            {
                this.properties["insertafter"] = value;
            }
        }
        public string InsertBefore
        {
            get
            {
                if (!this.properties.Contains("insertbefore"))
                {
                    return "";
                }
                return this.properties["insertbefore"];
            }
            set
            {
                this.properties["insertbefore"] = value;
            }
        }
        public string this[string key]
        {
            get
            {
                return this.properties[key];
            }
        }
        public Properties Properties
        {
            get
            {
                return this.properties;
            }
        }
        public ICondition[] Conditions
        {
            get
            {
                return this.conditions;
            }
        }
        public Codon(AddIn addIn, string name, Properties properties, ICondition[] conditions)
        {
            this.addIn = addIn;
            this.name = name;
            this.properties = properties;
            this.conditions = conditions;
        }
        public ConditionAction GetFailedAction(object caller)
        {
            return Condition.GetFailedAction(this.conditions, caller);
        }
        public ConditionAction GetCheckStateAction(object caller)
        {
            return Condition.GetCheckStateAction(this.conditions, caller);
        }
        public object BuildItem(object owner, System.Collections.ArrayList subItems)
        {
            IDoozer doozer;
            if (!AddInTree.Doozers.TryGetValue(this.Name, out doozer))
            {
                throw new CoreException("Doozer " + this.Name + " not found!");
            }
            if (!doozer.HandleConditions && this.conditions.Length > 0)
            {
                ConditionAction failedAction = this.GetFailedAction(owner);
                if (failedAction != ConditionAction.Nothing)
                {
                    return null;
                }
            }
            return doozer.BuildItem(owner, this, subItems);
        }
        public override string ToString()
        {
            return string.Format("[Codon: name = {0}, addIn={1}]", this.name, this.addIn.FileName);
        }
    }
}
