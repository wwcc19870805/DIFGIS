using System;
using System.Collections.Generic;
using System.Xml;
namespace ICSharpCode.Core
{
    public class Condition : ICondition
    {
        private string name;
        private Properties properties;
        private ConditionAction action;
        public ConditionAction Action
        {
            get
            {
                return this.action;
            }
            set
            {
                this.action = value;
            }
        }
        public string Name
        {
            get
            {
                return this.name;
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
        public Condition(string name, Properties properties)
        {
            this.name = name;
            this.properties = properties;
            this.action = properties.Get<ConditionAction>("action", ConditionAction.Exclude);
        }
        public bool IsValid(object caller)
        {
            try
            {
                return AddInTree.ConditionEvaluators[this.name].IsValid(caller, this);
            }
            catch (System.Collections.Generic.KeyNotFoundException)
            {
            }
            return false;
        }
        public static ICondition Read(XmlReader reader)
        {
            Properties properties = Properties.ReadFromAttributes(reader);
            string text = properties["name"];
            return new Condition(text, properties);
        }
        public static ICondition ReadComplexCondition(XmlReader reader)
        {
            Properties properties = Properties.ReadFromAttributes(reader);
            reader.Read();
            ICondition condition = null;
            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element:
                        switch (reader.LocalName)
                        {
                            case "And":
                                condition = AndCondition.Read(reader);
                                goto exit;
                            case "Or":
                                condition = OrCondition.Read(reader);
                                goto exit;
                            case "Not":
                                condition = NegatedCondition.Read(reader);
                                goto exit;
                            default://由错误提示可以看出ComplexCondition必须至少含有一个And、Or或者Not标签，后面就可以加Codon了
                                throw new AddInLoadException("Invalid element name '" + reader.LocalName
                                                             + "', the first entry in a ComplexCondition " +
                                                             "must be <And>, <Or> or <Not>");
                        }
                }
            }
        exit:
            if (condition != null)
            {
                ConditionAction action = properties.Get("action", ConditionAction.Exclude);
                condition.Action = action;
            }
            return condition;
        }
        public static ICondition[] ReadConditionList(XmlReader reader, string endElement)
        {
            List<ICondition> conditions = new List<ICondition>();
            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.EndElement:
                        if (reader.LocalName == endElement)
                        {
                            return conditions.ToArray();
                        }
                        break;
                    case XmlNodeType.Element:
                        switch (reader.LocalName)
                        {//前面三个case其实算是递归
                            case "And":
                                conditions.Add(AndCondition.Read(reader));
                                break;
                            case "Or":
                                conditions.Add(OrCondition.Read(reader));
                                break;
                            case "Not":
                                conditions.Add(NegatedCondition.Read(reader));
                                break;
                            case "Condition":
                                conditions.Add(Condition.Read(reader));
                                break;
                            default://由错误提示可以看出在And、Or或者Not标签中只能含有And、Or、Not或者Condition
                                throw new AddInLoadException("Invalid element name '" + reader.LocalName
                                                             + "', entries in a <" + endElement + "> " +
                                                             "must be <And>, <Or>, <Not> or <Condition>");
                        }
                        break;
                }
            }
            return conditions.ToArray();

        }
        public static ConditionAction GetFailedAction(System.Collections.Generic.IEnumerable<ICondition> conditionList, object caller)
        {
            ConditionAction result = ConditionAction.Nothing;
            foreach (ICondition current in conditionList)
            {
                if (current.Action != ConditionAction.Checked && current.Action != ConditionAction.UnChecked && !current.IsValid(caller))
                {
                    if (current.Action != ConditionAction.Disable)
                    {
                        return ConditionAction.Exclude;
                    }
                    result = ConditionAction.Disable;
                }
            }
            return result;
        }
        public static ConditionAction GetCheckStateAction(System.Collections.Generic.IEnumerable<ICondition> conditionList, object caller)
        {
            //ConditionAction result = ConditionAction.UnChecked;
            //foreach (ICondition current in conditionList)
            //{
            //    if (current.Action == ConditionAction.Checked || current.Action == ConditionAction.UnChecked)
            //    {
            //        if (current.IsValid(caller))
            //        {
            //            result = ConditionAction.Checked;
            //            break;
            //        }
            //        break;
            //    }
            //}
            //return result;
            ConditionAction result = ConditionAction.Nothing;
            foreach (ICondition current in conditionList)
            {
                if (current.Action == ConditionAction.Checked || current.Action == ConditionAction.UnChecked)
                {
                    if (current.IsValid(caller))
                    {
                        result = ConditionAction.Checked;
                        break;
                    }
                    else
                    {
                        result = ConditionAction.UnChecked;
                        break;
                    }
                }
            }
            return result;
        }
    }
}
