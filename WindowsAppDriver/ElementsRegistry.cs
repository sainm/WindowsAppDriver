using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using CommonLib;
using CommonLib.Exceptions;
using EngineLib.Elements;

namespace WindowsAppDriver
{
    internal class ElementsRegistry
    {
        private static int safeInstanceCount;


        private readonly Dictionary<string, WindowAppElement> registeredElements;


        public ElementsRegistry()
        {
            this.registeredElements = new Dictionary<string, WindowAppElement>();
        }


        public void Clear()
        {
            this.registeredElements.Clear();
        }

        public WindowAppElement GetRegisteredElement(string registeredKey)
        {
            var element = this.GetRegisteredElementOrNull(registeredKey);
            if (element != null)
            {
                return element;
            }

            throw new AutomationException("Stale element reference", ResponseStatus.StaleElementReference);
        }

        public string RegisterElement(WindowAppElement element)
        {
            var registeredKey =
                this.registeredElements.FirstOrDefault(
                    x => x.Value.Properties.RuntimeId == element.Properties.RuntimeId).Key;

            if (registeredKey == null)
            {
                Interlocked.Increment(ref safeInstanceCount);

                // TODO: Maybe use RuntimeId how registeredKey?
                registeredKey = element.GetHashCode() + "-"
                                                      + safeInstanceCount.ToString(string.Empty,
                                                          CultureInfo.InvariantCulture);
                this.registeredElements.Add(registeredKey, element);
            }

            return registeredKey;
        }

        public IEnumerable<string> RegisterElements(IEnumerable<WindowAppElement> elements)
        {
            return elements.Select(this.RegisterElement);
        }


        internal WindowAppElement GetRegisteredElementOrNull(string registeredKey)
        {
            WindowAppElement element;
            this.registeredElements.TryGetValue(registeredKey, out element);
            return element;
        }
    }
}