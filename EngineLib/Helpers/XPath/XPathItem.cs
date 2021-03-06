using System.Xml.XPath;

namespace EngineLib.Helpers.XPath
{


    internal abstract class XPathItem
    {

        internal abstract bool IsEmptyElement { get; }

        internal abstract string Name { get; }

        internal abstract XPathNodeType NodeType { get; }

        internal virtual string Value
        {
            get { return string.Empty; }
        }



        public abstract object TypedValue();



        internal abstract bool IsSamePosition(XPathItem item);

        internal abstract XPathItem MoveToFirstChild();

        internal abstract XPathItem MoveToFirstProperty();

        internal abstract XPathItem MoveToNext();

        internal abstract XPathItem MoveToNextProperty();

        internal abstract XPathItem MoveToParent();

        internal abstract XPathItem MoveToPrevious();

    }
}