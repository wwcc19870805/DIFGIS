using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace ICSharpCode.Core
{
    // 条件求反
    public class NegatedCondition : ICondition
    {
        /// <summary>
        /// 需要求反的条件引用
        /// </summary>
        ICondition condition;

        /// <summary>
        /// 需要求反的条件类型名称
        /// </summary>
        public string Name
        {
            get
            {
                return "Not " + condition.Name;
            }
        }

        ConditionAction action = ConditionAction.Exclude;

        /// <summary>
        /// 求反后，结果为false时的动作
        /// </summary>
        public ConditionAction Action
        {
            get
            {
                return action;
            }
            set
            {
                action = value;
            }
        }

        public NegatedCondition(ICondition condition)
        {
            this.condition = condition;
        }

        /// <summary>
        /// 计算条件求反
        /// </summary>
        public bool IsValid(object parameter)
        {
            return !condition.IsValid(parameter);
        }

        /// <summary>
        /// 不管<Not>标签下有几个条件，求反时只要第一个
        /// </summary>
        public static ICondition Read(XmlReader reader)
        {
            return new NegatedCondition(Condition.ReadConditionList(reader, "Not")[0]);
        }
    }
    
    // 条件求与
    public class AndCondition : ICondition
    {
        /// <summary>
        /// 参与计算与的条件集合
        /// </summary>
        ICondition[] conditions;

        /// <summary>
        /// 计算与的条件名称列表
        /// </summary>
        public string Name
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < conditions.Length; ++i)
                {
                    sb.Append(conditions[i].Name);
                    if (i + 1 < conditions.Length)
                    {
                        sb.Append(" And ");
                    }
                }
                return sb.ToString();
            }
        }

        ConditionAction action = ConditionAction.Exclude;

        /// <summary>
        /// 求与后，结果为false时的动作
        /// </summary>
        public ConditionAction Action
        {
            get
            {
                return action;
            }
            set
            {
                action = value;
            }
        }

        public AndCondition(ICondition[] conditions)
        {
            this.conditions = conditions;
        }

        /// <summary>
        /// 计算一组条件的与的值
        /// </summary>
        public bool IsValid(object parameter)
        {
            foreach (ICondition condition in conditions)
            {
                if (!condition.IsValid(parameter))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 读取<And>标签下的所有条件，并创建一个AndConditon对象
        /// </summary>
        public static ICondition Read(XmlReader reader)
        {
            return new AndCondition(Condition.ReadConditionList(reader, "And"));
        }
    }

    // 条件求或
    public class OrCondition : ICondition
    {
        /// <summary>
        /// 参与计算或的条件集合
        /// </summary>
        ICondition[] conditions;

        /// <summary>
        /// 计算或的条件名称列表
        /// </summary>
        public string Name
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < conditions.Length; ++i)
                {
                    sb.Append(conditions[i].Name);
                    if (i + 1 < conditions.Length)
                    {
                        sb.Append(" Or ");
                    }
                }
                return sb.ToString();
            }
        }

        ConditionAction action = ConditionAction.Exclude;

        /// <summary>
        /// 求或后，结果为false时的动作
        /// </summary>
        public ConditionAction Action
        {
            get
            {
                return action;
            }
            set
            {
                action = value;
            }
        }

        public OrCondition(ICondition[] conditions)
        {
            this.conditions = conditions;
        }

        /// <summary>
        /// 计算一组条件的或的值
        /// </summary>
        public bool IsValid(object parameter)
        {
            foreach (ICondition condition in conditions)
            {
                if (condition.IsValid(parameter))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 读取<Or>标签下的所有条件，并创建一个OrConditon对象
        /// </summary>
        public static ICondition Read(XmlReader reader)
        {
            return new OrCondition(Condition.ReadConditionList(reader, "Or"));
        }
    }

}
