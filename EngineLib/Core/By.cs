using System.Collections.Generic;
using System.Windows.Automation;

namespace EngineLib.Core
{
    public abstract class By
    {
        public static ByProperty AutomationProperty(AutomationProperty property, object value)
        {
            return AutomationProperty(TreeScope.Subtree, property, value);
        }


        public static ByProperty AutomationProperty(TreeScope scope, AutomationProperty property, object value)
        {
            return new ByProperty(scope, property, value);
        }


        public static ByProperty Name(string value)
        {
            return AutomationProperty(AutomationElement.NameProperty, value);
        }


        public static ByProperty Name(TreeScope scope, string value)
        {
            return AutomationProperty(scope, AutomationElement.NameProperty, value);
        }


        public static ByProperty Uid(string value)
        {
            return AutomationProperty(AutomationElement.AutomationIdProperty, value);
        }


        public static ByProperty Uid(TreeScope scope, string value)
        {
            return AutomationProperty(scope, AutomationElement.AutomationIdProperty, value);
        }


        public static ByXPath XPath(string value)
        {
            return new ByXPath(value);
        }


        public abstract override string ToString();


        internal abstract IEnumerable<AutomationElement> FindAll(AutomationElement parent, int timeout);

        internal abstract AutomationElement FindFirst(AutomationElement parent, int timeout);
    }
}