using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Windows;
using System.Windows.Forms;
using WindowsInput;
using EngineLib.Helpers;

namespace EngineLib.Core
{
    public class MouseSimulatorExt
    {
        private readonly IMouseSimulator mouseSimulator;


        internal MouseSimulatorExt(IMouseSimulator mouseSimulator)
        {
            this.mouseSimulator = mouseSimulator;
        }


        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Reviewed.")]
        public Point CurrentCursorPos
        {
            get
            {
                var currentPoint = Cursor.Position;
                return new Point(currentPoint.X, currentPoint.Y);
            }
        }


        public void Click(EngineLib.Core.MouseButton button)
        {
            switch (button)
            {
                case EngineLib.Core.MouseButton.Left:
                    this.LeftButtonClick();
                    break;
                case EngineLib.Core.MouseButton.Right:
                    this.RightButtonClick();
                    break;
            }
        }


        public void Click(EngineLib.Core.MouseButton button, double x, double y)
        {
            this.SetCursorPos(x, y);
            this.Click(button);
        }


        public void DoubleClick(EngineLib.Core.MouseButton button)
        {
            switch (button)
            {
                case EngineLib.Core.MouseButton.Left:
                    this.LeftButtonDoubleClick();
                    break;
                case EngineLib.Core.MouseButton.Right:
                    this.RightButtonDoubleClick();
                    break;
            }
        }


        public void DoubleClick(EngineLib.Core.MouseButton button, double x, double y)
        {
            this.SetCursorPos(x, y);
            this.DoubleClick(button);
        }

        /// <summary>
        public void LeftButtonClick()
        {
            this.mouseSimulator.LeftButtonClick();
            Thread.Sleep(250);
        }


        public void LeftButtonDoubleClick()
        {
            this.mouseSimulator.LeftButtonDoubleClick();
            Thread.Sleep(250);
        }


        public void MoveCursorPos(double x, double y)
        {
            var currentPoint = this.CurrentCursorPos;
            this.SetCursorPos(currentPoint.X + x, currentPoint.Y + y);
        }

        public void RightButtonClick()
        {
            this.mouseSimulator.RightButtonClick();
            Thread.Sleep(250);
        }


        public void RightButtonDoubleClick()
        {
            this.mouseSimulator.RightButtonDoubleClick();
            Thread.Sleep(250);
        }


        public void SetCursorPos(double x, double y)
        {
            var virtualScreenPoint = ScreenCoordinatesHelper.ScreenPointToVirtualScreenPoint(new Point(x, y));
            this.mouseSimulator.MoveMouseToPositionOnVirtualDesktop(virtualScreenPoint.X, virtualScreenPoint.Y);
            Thread.Sleep(250);
        }


        public void VerticalScroll(int amountOfClicks)
        {
            this.mouseSimulator.VerticalScroll(amountOfClicks);
            Thread.Sleep(250);
        }
    }
}