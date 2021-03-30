using System;
using System.Collections.Generic;
using System.Windows.Automation;
using EngineLib.Core;
using EngineLib.Exceptions;
using EngineLib.Extensions;
using NLog;

namespace EngineLib.Elements
{
    public class WindowAppElement : IEquatable<WindowAppElement>
    {
        protected static readonly Logger Logger = CruciatusFactory.Logger;


        private AutomationElement instance;


        public WindowAppElement(WindowAppElement element)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }

            this.Instance = element.Instance;
            this.Parent = element;
            this.FindStrategy = element.FindStrategy;
        }


        public WindowAppElement(WindowAppElement parent, By findStrategy)
        {
            if (parent == null)
            {
                throw new ArgumentNullException("parent");
            }

            this.Parent = parent;
            this.FindStrategy = findStrategy;
        }

        internal WindowAppElement(WindowAppElement parent, AutomationElement element, By findStrategy)
        {
            this.Parent = parent;
            this.Instance = element;
            this.FindStrategy = findStrategy;
        }


        public By FindStrategy { get; internal set; }


        public bool IsStale
        {
            get
            {
                try
                {
                    this.Instance.GetCurrentPropertyValue(AutomationElement.AutomationIdProperty);
                    return false;
                }
                catch (NoSuchElementException)
                {
                    return true;
                }
                catch (ElementNotAvailableException)
                {
                    return true;
                }
            }
        }


        public CruciatusElementProperties Properties
        {
            get { return new CruciatusElementProperties(this.Instance); }
        }


        internal AutomationElement Instance
        {
            get
            {
                if (this.instance == null)
                {
                    var element = this.Parent.FindElement(this.FindStrategy);
                    this.instance = element != null ? element.Instance : null;
                }

                if (this.instance == null)
                {
                    throw new NoSuchElementException("ELEMENT NOT FOUND");
                }

                return this.instance;
            }

            set { this.instance = value; }
        }

        internal WindowAppElement Parent { get; set; }


        public void Click()
        {
            this.Click(CruciatusFactory.Settings.ClickButton);
        }


        public void Click(MouseButton button)
        {
            this.Click(button, ClickStrategies.None, false);
        }

        public void Click(MouseButton button, ClickStrategies strategy)
        {
            this.Click(button, strategy, false);
        }


        public void Click(MouseButton button, ClickStrategies strategy, bool doubleClick)
        {
            if (!this.Instance.Current.IsEnabled)
            {
                Logger.Error("Element '{0}' not enabled. Click failed.", this.ToString());
                CruciatusFactory.Screenshoter.AutomaticScreenshotCaptureIfNeeded();
                throw new ElementNotEnabledException("NOT CLICK");
            }

            if (strategy == ClickStrategies.None)
            {
                strategy = ~strategy;
            }

            if (strategy.HasFlag(ClickStrategies.ClickablePoint))
            {
                if (CruciatusCommand.TryClickOnClickablePoint(button, this, doubleClick))
                {
                    return;
                }
            }

            if (strategy.HasFlag(ClickStrategies.BoundingRectangleCenter))
            {
                if (CruciatusCommand.TryClickOnBoundingRectangleCenter(button, this, doubleClick))
                {
                    return;
                }
            }

            if (strategy.HasFlag(ClickStrategies.InvokePattern))
            {
                if (CruciatusCommand.TryClickUsingInvokePattern(this, doubleClick))
                {
                    return;
                }
            }

            Logger.Error("Click on '{0}' element failed", this.ToString());
            throw new CruciatusException("NOT CLICK");
        }


        public void DoubleClick()
        {
            this.DoubleClick(CruciatusFactory.Settings.ClickButton);
        }


        public void DoubleClick(MouseButton button)
        {
            this.DoubleClick(button, ClickStrategies.None);
        }


        public void DoubleClick(MouseButton button, ClickStrategies strategy)
        {
            this.Click(button, strategy, true);
        }

        public bool Equals(WindowAppElement other)
        {
            return other != null && this.Instance.Equals(other.Instance);
        }

        public override bool Equals(object obj)
        {
            var cruciatusElement = obj as WindowAppElement;
            return cruciatusElement != null && this.Equals(cruciatusElement);
        }


        public virtual WindowAppElement FindElement(By strategy)
        {
            return CruciatusCommand.FindFirst(this, strategy);
        }


        public virtual WindowAppElement FindElementByName(string value)
        {
            return this.FindElement(By.Name(value));
        }


        public virtual WindowAppElement FindElementByUid(string value)
        {
            return this.FindElement(By.Uid(value));
        }


        public IEnumerable<WindowAppElement> FindElements(By strategy)
        {
            return CruciatusCommand.FindAll(this, strategy);
        }

        public override int GetHashCode()
        {
            return this.Instance.GetHashCode();
        }


        public void SetFocus()
        {
            if (!this.Instance.Current.IsEnabled)
            {
                Logger.Error("Element '{0}' not enabled. Set focus failed.", this.ToString());
                CruciatusFactory.Screenshoter.AutomaticScreenshotCaptureIfNeeded();
                throw new ElementNotEnabledException("NOT SET FOCUS");
            }

            if (this.Instance.Current.ControlType.Equals(ControlType.Window))
            {
                object windowPatternObject;
                if (this.Instance.TryGetCurrentPattern(WindowPattern.Pattern, out windowPatternObject))
                {
                    ((WindowPattern) windowPatternObject).SetWindowVisualState(WindowVisualState.Normal);
                    return;
                }
            }

            try
            {
                this.Instance.SetFocus();
            }
            catch (InvalidOperationException exception)
            {
                Logger.Error("Set focus on element '{0}' failed.", this.ToString());
                Logger.Debug(exception);
                throw new CruciatusException("NOT SET FOCUS");
            }
        }


        public void SetText(string text)
        {
            if (!this.Instance.Current.IsEnabled)
            {
                Logger.Error("Element '{0}' not enabled. Set text failed.", this.ToString());
                CruciatusFactory.Screenshoter.AutomaticScreenshotCaptureIfNeeded();
                throw new ElementNotEnabledException("NOT SET TEXT");
            }

            this.Click(MouseButton.Left, ClickStrategies.ClickablePoint | ClickStrategies.BoundingRectangleCenter);

            CruciatusFactory.Keyboard.SendCtrlA().SendBackspace().SendText(text);
        }


        public string Text()
        {
            return this.Text(GetTextStrategies.None);
        }


        public string Text(GetTextStrategies strategy)
        {
            if (strategy == GetTextStrategies.None)
            {
                strategy = ~strategy;
            }

            string text;
            if (strategy.HasFlag(GetTextStrategies.TextPattern))
            {
                if (CruciatusCommand.TryGetTextUsingTextPattern(this, out text))
                {
                    return text;
                }
            }

            if (strategy.HasFlag(GetTextStrategies.ValuePattern))
            {
                if (CruciatusCommand.TryGetTextUsingValuePattern(this, out text))
                {
                    return text;
                }
            }

            Logger.Error("Get text from '{0}' element failed.", this.ToString());
            CruciatusFactory.Screenshoter.AutomaticScreenshotCaptureIfNeeded();
            throw new CruciatusException("NO GET TEXT");
        }


        public override string ToString()
        {
            var typeName = this.Instance.Current.ControlType.ProgrammaticName;
            var uid = this.Instance.Current.AutomationId;
            var name = this.Instance.Current.Name;
            var str = string.Format(
                "{0}{1}{2}",
                "type: " + typeName,
                string.IsNullOrEmpty(uid) ? string.Empty : ", uid: " + uid,
                string.IsNullOrEmpty(name) ? string.Empty : ", name: " + name);
            return str;
        }
    }
}