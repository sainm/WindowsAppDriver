using System.Windows.Automation;
using EngineLib.Core;
using EngineLib.Extensions;

namespace EngineLib.Elements
{
    public class ListBox : WindowAppElement
    {
        public ListBox(WindowAppElement element)
            : base(element)
        {
        }


        public ListBox(WindowAppElement parent, By getStrategy)
            : base(parent, getStrategy)
        {
        }

        public WindowAppElement ScrollTo(By getStrategy)
        {
            if (!this.Instance.Current.IsEnabled)
            {
                Logger.Error("Element '{0}' not enabled. Scroll failed.", this.ToString());
                CruciatusFactory.Screenshoter.AutomaticScreenshotCaptureIfNeeded();
                throw new ElementNotEnabledException("NOT SCROLL");
            }

            var scrollPattern = this.Instance.GetCurrentPattern(ScrollPattern.Pattern) as ScrollPattern;
            if (scrollPattern == null)
            {
                Logger.Debug("{0} does not support ScrollPattern.", this);
                throw new ElementNotEnabledException("NOT SCROLL");
            }

            var element = CruciatusCommand.FindFirst(this, getStrategy, 1000);

            if (element == null && scrollPattern.Current.VerticallyScrollable)
            {
                while (scrollPattern.Current.VerticalScrollPercent > 0.1)
                {
                    scrollPattern.ScrollVertical(ScrollAmount.LargeDecrement);
                }

                if (scrollPattern.Current.HorizontallyScrollable)
                {
                    while (scrollPattern.Current.HorizontalScrollPercent > 0.1)
                    {
                        scrollPattern.ScrollHorizontal(ScrollAmount.LargeDecrement);
                    }
                }

                while (element == null && scrollPattern.Current.VerticalScrollPercent < 99.9)
                {
                    scrollPattern.ScrollVertical(ScrollAmount.LargeIncrement);
                    element = CruciatusCommand.FindFirst(this, getStrategy, 1000);
                }
            }

            if (element == null)
            {
                Logger.Debug("No elements matching {1} were found in {0}.", this, getStrategy);
                return null;
            }

            while (element.Instance.ClickablePointUnder(this.Instance, scrollPattern))
            {
                scrollPattern.ScrollVertical(ScrollAmount.SmallIncrement);
            }

            while (element.Instance.ClickablePointOver(this.Instance))
            {
                scrollPattern.ScrollVertical(ScrollAmount.SmallDecrement);
            }

            while (element.Instance.ClickablePointRight(this.Instance, scrollPattern))
            {
                scrollPattern.ScrollHorizontal(ScrollAmount.SmallIncrement);
            }

            while (element.Instance.ClickablePointLeft(this.Instance))
            {
                scrollPattern.ScrollHorizontal(ScrollAmount.SmallDecrement);
            }

            return element;
        }
    }
}