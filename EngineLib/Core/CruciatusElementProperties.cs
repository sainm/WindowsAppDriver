using System.Windows;
using System.Windows.Automation;

namespace EngineLib.Core
{
    public class CruciatusElementProperties
    {
        private readonly AutomationElement _element;


        internal CruciatusElementProperties(AutomationElement element)
        {
            this._element = element;
        }


        public Rect BoundingRectangle => this._element.Current.BoundingRectangle;

        public Point? ClickablePoint
        {
            get
            {
                Point point;
                var exists = this._element.TryGetClickablePoint(out point);
                return exists ? point : new Point?();
            }
        }


        public bool IsEnabled => this._element.Current.IsEnabled;


        public bool IsOffscreen => this._element.Current.IsOffscreen;


        public string Name => this._element.Current.Name;


        public string RuntimeId => string.Join(" ", this._element.GetRuntimeId());
    }
}