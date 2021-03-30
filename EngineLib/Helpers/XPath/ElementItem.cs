using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Automation;
using System.Xml.XPath;
using EngineLib.Elements;
using EngineLib.Exceptions;

namespace EngineLib.Helpers.XPath
{


    internal class ElementItem : XPathItem
    {
       

        private readonly AutomationElement element;

        private readonly TreeWalker treeWalker = TreeWalker.ControlViewWalker;

        private List<AutomationProperty> properties;

       

        internal ElementItem(AutomationElement element)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }

            this.element = element;
        }

      

        internal override bool IsEmptyElement
        {
            get
            {
                var hasChild = this.MoveToFirstChild() != null;
                if (hasChild)
                {
                    return false;
                }

                try
                {
                    new WindowAppElement(null, this.element, null).Text();
                    return false;
                }
                catch (CruciatusException)
                {
                    return true;
                }
            }
        }

        internal override string Name
        {
            get
            {
                return this.element.Current.Name;
            }
        }

        internal override XPathNodeType NodeType
        {
            get
            {
                return XPathNodeType.Element;
            }
        }

        internal List<AutomationProperty> SupportedProperties
        {
            get
            {
                return this.properties ?? (this.properties = this.element.GetSupportedProperties().ToList());
            }
        }


        public override object TypedValue()
        {
            return this.element;
        }

       

        internal static XPathItem Create(AutomationElement instance)
        {
            return instance.Equals(AutomationElement.RootElement) ? new RootItem() : new ElementItem(instance);
        }

        internal AutomationProperty GetNextPropertyOrNull(AutomationProperty property)
        {
            var index = this.SupportedProperties.IndexOf(property);
            return this.SupportedProperties.ElementAtOrDefault(index + 1);
        }

        internal object GetPropertyValue(AutomationProperty property)
        {
            return this.element.GetCurrentPropertyValue(property);
        }

        internal override bool IsSamePosition(XPathItem item)
        {
            var obj = item as ElementItem;
            return obj != null && obj.element.Equals(this.element);
        }

        internal override XPathItem MoveToFirstChild()
        {
            var firstChild = this.treeWalker.GetFirstChild(this.element);
            return (firstChild == null) ? null : Create(firstChild);
        }

        internal override XPathItem MoveToFirstProperty()
        {
            return this.SupportedProperties.Any() ? new PropertyItem(this, this.SupportedProperties[0]) : null;
        }

        internal override XPathItem MoveToNext()
        {
            var next = this.treeWalker.GetNextSibling(this.element);
            return (next == null) ? null : Create(next);
        }

        internal override XPathItem MoveToNextProperty()
        {
            return null;
        }

        internal override XPathItem MoveToParent()
        {
            var parent = this.treeWalker.GetParent(this.element);
            return (parent == null) ? null : Create(parent);
        }

        internal override XPathItem MoveToPrevious()
        {
            var previous = this.treeWalker.GetPreviousSibling(this.element);
            return (previous == null) ? null : Create(previous);
        }

    }
}
