using System.Windows.Automation;
using EngineLib.Core;
using EngineLib.Extensions;

namespace EngineLib.Elements
{
    public class CheckBox : WindowAppElement
    {
        public CheckBox(WindowAppElement element)
            : base(element)
        {
        }


        public CheckBox(WindowAppElement parent, By getStrategy)
            : base(parent, getStrategy)
        {
        }


        public bool IsToggleOn
        {
            get { return this.ToggleState == ToggleState.On; }
        }


        internal ToggleState ToggleState
        {
            get { return this.GetAutomationPropertyValue<ToggleState>(TogglePattern.ToggleStateProperty); }
        }


        public void Check()
        {
            if (this.IsToggleOn)
            {
                return;
            }

            this.Click();
        }


        public void Uncheck()
        {
            if (!this.IsToggleOn)
            {
                return;
            }

            this.Click();
        }
    }
}