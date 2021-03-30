using System.Threading;
using System.Windows.Automation;
using EngineLib.Core;
using EngineLib.Exceptions;
using EngineLib.Extensions;

namespace EngineLib.Elements
{

    public class ComboBox : WindowAppElement
    {
        public ComboBox(WindowAppElement element)
            : base(element)
        {
        }


        public ComboBox(WindowAppElement parent, By getStrategy)
            : base(parent, getStrategy)
        {
        }


        public bool IsExpanded
        {
            get { return this.ExpandCollapseState == ExpandCollapseState.Expanded; }
        }


        internal ExpandCollapseState ExpandCollapseState
        {
            get
            {
                return
                    this.GetAutomationPropertyValue<ExpandCollapseState>(
                        ExpandCollapsePattern.ExpandCollapseStateProperty);
            }
        }


        public void Collapse()
        {
            this.Collapse(ExpandStrategy.Click);
        }


        public void Collapse(ExpandStrategy strategy)
        {
            if (this.ExpandCollapseState == ExpandCollapseState.Collapsed)
            {
                return;
            }

            switch (strategy)
            {
                case ExpandStrategy.Click:
                    this.Click();
                    break;
                case ExpandStrategy.ExpandCollapsePattern:
                    this.Instance.GetPattern<ExpandCollapsePattern>(ExpandCollapsePattern.Pattern).Collapse();
                    break;
                default:
                    Logger.Error("{0} is not valid or implemented collapse strategy.", strategy);
                    throw new CruciatusException("NOT COLLAPSE");
            }

            Thread.Sleep(250);
        }


        public void Expand()
        {
            this.Expand(ExpandStrategy.Click);
        }


        public void Expand(ExpandStrategy strategy)
        {
            if (this.ExpandCollapseState == ExpandCollapseState.Expanded)
            {
                return;
            }

            switch (strategy)
            {
                case ExpandStrategy.Click:
                    this.Click();
                    break;
                case ExpandStrategy.ExpandCollapsePattern:
                    this.Instance.GetPattern<ExpandCollapsePattern>(ExpandCollapsePattern.Pattern).Expand();
                    break;
                default:
                    Logger.Error("{0} is not valid or implemented expand strategy.", strategy);
                    throw new CruciatusException("NOT EXPAND");
            }

            Thread.Sleep(250);
        }


        public WindowAppElement ScrollTo(By getStrategy)
        {
            if (!this.Instance.Current.IsEnabled)
            {
                Logger.Error("Element '{0}' not enabled. Scroll failed.", this.ToString());
                CruciatusFactory.Screenshoter.AutomaticScreenshotCaptureIfNeeded();
                throw new ElementNotEnabledException("NOT SCROLL");
            }

            if (this.ExpandCollapseState != ExpandCollapseState.Expanded)
            {
                Logger.Error("Element {0} is not opened.", this);
                throw new CruciatusException("NOT SCROLL");
            }

            var scrollPattern = this.Instance.GetCurrentPattern(ScrollPattern.Pattern) as ScrollPattern;
            if (scrollPattern == null)
            {
                Logger.Error("{0} does not support ScrollPattern.", this);
                throw new CruciatusException("NOT SCROLL");
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

            var strategy =
                By.AutomationProperty(TreeScope.Subtree, AutomationElement.ClassNameProperty, "Popup")
                    .And(AutomationElement.ProcessIdProperty, this.Instance.Current.ProcessId);
            var popupWindow = CruciatusFactory.Root.FindElement(strategy);
            if (popupWindow == null)
            {
                Logger.Error("Popup window of drop-down list was not found.");
                throw new CruciatusException("NOT SCROLL");
            }

            var popupWindowInstance = popupWindow.Instance;
            while (element.Instance.ClickablePointUnder(popupWindowInstance, scrollPattern))
            {
                scrollPattern.ScrollVertical(ScrollAmount.LargeIncrement);
            }

            while (element.Instance.ClickablePointOver(popupWindowInstance))
            {
                scrollPattern.ScrollVertical(ScrollAmount.SmallDecrement);
            }

            while (element.Instance.ClickablePointRight(popupWindowInstance, scrollPattern))
            {
                scrollPattern.ScrollHorizontal(ScrollAmount.LargeIncrement);
            }

            while (element.Instance.ClickablePointLeft(popupWindowInstance))
            {
                scrollPattern.ScrollHorizontal(ScrollAmount.SmallDecrement);
            }

            return element;
        }


        public WindowAppElement SelectedItem()
        {
            return
                this.FindElement(
                    By.AutomationProperty(TreeScope.Subtree, SelectionItemPattern.IsSelectedProperty, true));
        }
    }
}