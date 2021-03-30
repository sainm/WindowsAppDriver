using System.Collections.Generic;
using System.Windows.Automation;
using EngineLib.Exceptions;
using EngineLib.Helpers;

namespace EngineLib.Core
{
    #region using

    #endregion

    internal enum ConditionType
    {
        None,

        Or,

        And
    }

    internal struct Info
    {

        internal ConditionType ConditionType;

        internal AutomationProperty Property;

        internal object Value;



        internal Info(AutomationProperty property, object value, ConditionType conditionType)
        {
            this.Property = property;
            this.Value = value;
            this.ConditionType = conditionType;
        }

    }


    public class ByProperty : By
    {
        private readonly List<Info> infoList;

        private readonly TreeScope scope;


        internal ByProperty(TreeScope scope, AutomationProperty property, object value)
        {
            this.scope = scope;
            this.infoList = new List<Info> {new Info(property, value, ConditionType.None)};
        }


        public ByProperty And(AutomationProperty property, object value)
        {
            this.infoList.Add(new Info(property, value, ConditionType.And));
            return this;
        }


        public ByProperty AndType(ControlType value)
        {
            this.And(AutomationElement.ControlTypeProperty, value);
            return this;
        }


        public ByProperty Or(AutomationProperty property, object value)
        {
            this.infoList.Add(new Info(property, value, ConditionType.Or));
            return this;
        }


        public ByProperty OrName(string value)
        {
            this.Or(AutomationElement.NameProperty, value);
            return this;
        }

        public override string ToString()
        {
            var info = this.infoList[0];
            var str = AutomationPropertyHelper.GetPropertyName(info.Property) + ": " + info.Value;
            for (var i = 1; i < this.infoList.Count; ++i)
            {
                info = this.infoList[i];
                var condition = info.ConditionType.ToString().ToLower();
                var propertyName = AutomationPropertyHelper.GetPropertyName(info.Property);
                var propertyKeyValue = propertyName + ": " + info.Value;
                str = $"({str}) {condition} {propertyKeyValue}";
            }

            return str;
        }


        internal override IEnumerable<AutomationElement> FindAll(AutomationElement parent, int timeout)
        {
            return AutomationElementHelper.FindAll(parent, this.scope, this.GetCondition(), timeout);
        }

        internal override AutomationElement FindFirst(AutomationElement parent, int timeout)
        {
            return AutomationElementHelper.FindFirst(parent, this.scope, this.GetCondition(), timeout);
        }

        private Condition GetCondition()
        {
            var info = this.infoList[0];
            Condition result = new PropertyCondition(info.Property, info.Value);
            for (var i = 1; i < this.infoList.Count; ++i)
            {
                info = this.infoList[i];
                var condition = new PropertyCondition(info.Property, info.Value);
                switch (info.ConditionType)
                {
                    case ConditionType.And:
                        result = new AndCondition(result, condition);
                        break;

                    case ConditionType.Or:
                        result = new OrCondition(result, condition);
                        break;

                    default:
                        throw new CruciatusException("ConditionType ERROR");
                }
            }

            return result;
        }
    }
}