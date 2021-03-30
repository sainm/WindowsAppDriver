using System.Windows.Automation;
using EngineLib.Core;
using EngineLib.Exceptions;
using EngineLib.Extensions;
using EngineLib.Helpers;


namespace EngineLib.Elements
{
    public class DataGrid : WindowAppElement
    {
        public DataGrid(WindowAppElement parent, By getStrategy)
            : base(parent, getStrategy)
        {
        }

        public DataGrid(WindowAppElement element)
            : base(element)
        {
        }


        public int ColumnCount
        {
            get { return this.GetAutomationPropertyValue<int>(GridPattern.ColumnCountProperty); }
        }


        public int RowCount
        {
            get { return this.GetAutomationPropertyValue<int>(GridPattern.RowCountProperty); }
        }


        public virtual WindowAppElement Item(int row, int column)
        {
            if (!this.Instance.Current.IsEnabled)
            {
                Logger.Error("Element '{0}' not enabled. Scroll failed.", this.ToString());
                CruciatusFactory.Screenshoter.AutomaticScreenshotCaptureIfNeeded();
                throw new ElementNotEnabledException("NOT GET ITEM");
            }


            if (row < 0 || column < 0)
            {
                Logger.Error("Cell index [{1}, {2}] is out of bounds for DataGrid {0}.", this, row, column);
                throw new CruciatusException("NOT GET ITEM");
            }


            var cellCondition =
                new AndCondition(
                    new PropertyCondition(AutomationElement.IsGridItemPatternAvailableProperty, true),
                    new PropertyCondition(GridItemPattern.RowProperty, row),
                    new PropertyCondition(GridItemPattern.ColumnProperty, column));
            var cell = AutomationElementHelper.FindFirst(this.Instance, TreeScope.Subtree, cellCondition);


            if (cell == null || !this.Instance.ContainsClickablePoint(cell))
            {
                Logger.Error("Cell [{1}, {2}] is not visible in DataGrid {0}.", this, row, column);
                throw new CruciatusException("NOT GET ITEM");
            }

            var elem = cell.FindFirst(TreeScope.Subtree, Condition.TrueCondition);
            if (elem == null)
            {
                Logger.Error("Item not found in cell [{1}, {2}] for DataGrid {0}.", this, row, column);
                throw new CruciatusException("NOT GET ITEM");
            }

            return new WindowAppElement(null, elem, null);
        }


        public virtual void ScrollTo(int row, int column)
        {
            if (!this.Instance.Current.IsEnabled)
            {
                Logger.Error("Element '{0}' not enabled. Scroll failed.", this.ToString());
                CruciatusFactory.Screenshoter.AutomaticScreenshotCaptureIfNeeded();
                throw new ElementNotEnabledException("NOT SCROLL");
            }

            if (row < 0 || column < 0)
            {
                var msg = string.Format("Cell index [{1}, {2}] is out of bounds for DataGrid {0}.", this, row, column);
                Logger.Error(msg);
                throw new CruciatusException("NOT SCROLL");
            }

            var scrollPattern = this.Instance.GetCurrentPattern(ScrollPattern.Pattern) as ScrollPattern;
            if (scrollPattern == null)
            {
                Logger.Error("{0} does not support ScrollPattern.", this.ToString());
                throw new CruciatusException("NOT SCROLL");
            }

            var cellCondition =
                new AndCondition(
                    new PropertyCondition(AutomationElement.IsGridItemPatternAvailableProperty, true),
                    new PropertyCondition(GridItemPattern.RowProperty, row));

            var cell = AutomationElementHelper.FindFirst(this.Instance, TreeScope.Subtree, cellCondition);

            if (cell == null && scrollPattern.Current.VerticallyScrollable)
            {
                while (scrollPattern.Current.VerticalScrollPercent > 0.1)
                {
                    scrollPattern.ScrollVertical(ScrollAmount.LargeIncrement);
                }

                if (scrollPattern.Current.HorizontallyScrollable)
                {
                    while (scrollPattern.Current.HorizontalScrollPercent > 0.1)
                    {
                        scrollPattern.ScrollHorizontal(ScrollAmount.LargeIncrement);
                    }
                }

                while (cell == null && scrollPattern.Current.VerticalScrollPercent < 99.9)
                {
                    scrollPattern.ScrollVertical(ScrollAmount.LargeIncrement);
                    cell = AutomationElementHelper.FindFirst(this.Instance, TreeScope.Subtree, cellCondition);
                }
            }

            if (cell == null)
            {
                Logger.Error("Row index {1} is out of bounds for {0}.", this, row);
                throw new CruciatusException("NOT SCROLL");
            }

            while (cell.ClickablePointUnder(this.Instance, scrollPattern))
            {
                scrollPattern.ScrollVertical(ScrollAmount.SmallIncrement);
            }

            while (cell.ClickablePointOver(this.Instance))
            {
                scrollPattern.ScrollVertical(ScrollAmount.SmallDecrement);
            }

            cellCondition =
                new AndCondition(
                    new PropertyCondition(AutomationElement.IsGridItemPatternAvailableProperty, true),
                    new PropertyCondition(GridItemPattern.RowProperty, row),
                    new PropertyCondition(GridItemPattern.ColumnProperty, column));

            cell = AutomationElementHelper.FindFirst(this.Instance, TreeScope.Subtree, cellCondition);

            if (cell == null && scrollPattern.Current.HorizontallyScrollable)
            {
                while (cell == null && scrollPattern.Current.HorizontalScrollPercent < 99.9)
                {
                    scrollPattern.ScrollHorizontal(ScrollAmount.LargeIncrement);
                    cell = AutomationElementHelper.FindFirst(this.Instance, TreeScope.Subtree, cellCondition);
                }
            }

            if (cell == null)
            {
                Logger.Error("Column index {1} is out of bounds for DataGrid {0}.", this, column);
                throw new CruciatusException("NOT SCROLL");
            }

            while (cell.ClickablePointRight(this.Instance, scrollPattern))
            {
                scrollPattern.ScrollHorizontal(ScrollAmount.SmallIncrement);
            }

            while (cell.ClickablePointLeft(this.Instance))
            {
                scrollPattern.ScrollHorizontal(ScrollAmount.SmallDecrement);
            }
        }


        public virtual void SelectCell(int row, int column)
        {
            var cell = this.Item(row, column);
            if (cell == null)
            {
                Logger.Error("Cell index [{1}, {2}] is out of bounds for DataGrid {0}.", this, row, column);
                throw new CruciatusException("NOT SELECT CELL");
            }

            cell.Click();
        }
    }
}