using System.Windows.Automation;
using System.Xml.XPath;

namespace EngineLib.Helpers.XPath
{
    internal class PropertyItem : XPathItem
    {
        private readonly ElementItem parent;

        private readonly AutomationProperty property;


        public PropertyItem(ElementItem parent, AutomationProperty property)
        {
            this.parent = parent;
            this.property = property;
        }


        internal override bool IsEmptyElement
        {
            get { return false; }
        }

        internal override string Name
        {
            get { return AutomationPropertyHelper.GetPropertyName(this.property); }
        }

        internal override XPathNodeType NodeType
        {
            get { return XPathNodeType.Attribute; }
        }

        internal override string Value
        {
            get
            {
                var value = this.TypedValue();
                var type = value as ControlType;
                return type != null ? type.ProgrammaticName : value.ToString();
            }
        }


        public override object TypedValue()
        {
            return this.parent.GetPropertyValue(this.property);
        }


        internal override bool IsSamePosition(XPathItem item)
        {
            var obj = item as PropertyItem;
            return obj != null && obj.parent == this.parent && obj.property.Equals(this.property);
        }

        internal override XPathItem MoveToFirstChild()
        {
            return null;
        }

        internal override XPathItem MoveToFirstProperty()
        {
            return null;
        }

        internal override XPathItem MoveToNext()
        {
            return null;
        }

        internal override XPathItem MoveToNextProperty()
        {
            var nextProperty = this.parent.GetNextPropertyOrNull(this.property);
            return (nextProperty == null) ? null : new PropertyItem(this.parent, nextProperty);
        }

        internal override XPathItem MoveToParent()
        {
            return this.parent;
        }

        internal override XPathItem MoveToPrevious()
        {
            return null;
        }
    }
}