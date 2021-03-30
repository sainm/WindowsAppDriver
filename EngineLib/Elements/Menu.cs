using System;
using System.Linq;
using EngineLib.Core;
using EngineLib.Exceptions;
using EngineLib.Extensions;

namespace EngineLib.Elements
{
    public class Menu : WindowAppElement
    {
        public Menu(WindowAppElement element)
            : base(element)
        {
        }

        public Menu(WindowAppElement parent, By getStrategy)
            : base(parent, getStrategy)
        {
        }

        public WindowAppElement GetItem(string headersPath)
        {
            if (string.IsNullOrEmpty(headersPath))
            {
                throw new ArgumentNullException("headersPath");
            }

            var item = (WindowAppElement) this;
            var headers = headersPath.Split('$');
            for (var i = 0; i < headers.Length - 1; ++i)
            {
                var name = headers[i];
                item = item.FindElement(By.Name(name));
                if (item == null)
                {
                    Logger.Error("Item '{0}' not found. Find item failed.", name);
                    throw new CruciatusException("NOT GET ITEM");
                }

                item.Click();
            }

            return item.FindElement(By.Name(headers.Last()));
        }


        public virtual void SelectItem(string headersPath)
        {
            if (!this.Instance.Current.IsEnabled)
            {
                Logger.Error("Element '{0}' not enabled. Select item failed.", this.ToString());
                CruciatusFactory.Screenshoter.AutomaticScreenshotCaptureIfNeeded();
                throw new CruciatusException("NOT SELECT ITEM");
            }

            var item = this.GetItem(headersPath);
            if (item == null)
            {
                Logger.Error("Item '{0}' not found. Select item failed.", headersPath);
                throw new CruciatusException("NOT SELECT ITEM");
            }

            item.Click();
        }
    }
}