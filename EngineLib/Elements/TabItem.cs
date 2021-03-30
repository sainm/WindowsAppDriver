using System.Windows.Automation;
using EngineLib.Core;
using EngineLib.Extensions;

namespace EngineLib.Elements
{
    public class TabItem : WindowAppElement
    {
        public TabItem(WindowAppElement parent, By getStrategy)
            : base(parent, getStrategy)
        {
        }

        public bool IsSelection
        {
            get { return this.GetAutomationPropertyValue<bool>(SelectionItemPattern.IsSelectedProperty); }
        }


        public void Select()
        {
            if (this.IsSelection)
            {
                return;
            }

            this.Click();
        }
    }
}