using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Automation;
using WindowsInput.Native;
using EngineLib.Elements;
using EngineLib.Exceptions;

namespace EngineLib.Extensions
{
    public static class CruciatusElementExtension
    {
        public static void ClickWithPressedCtrl(this WindowAppElement element)
        {
            ClickWithPressedKeys(element, new List<VirtualKeyCode> {VirtualKeyCode.CONTROL});
        }


        [SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods",
            Justification = "First parameter in extension cannot be null.")]
        public static void ClickWithPressedKeys(this WindowAppElement element, List<VirtualKeyCode> keys)
        {
            keys.ForEach(key => CruciatusFactory.Keyboard.KeyDown(key));
            element.Click();
            keys.ForEach(key => CruciatusFactory.Keyboard.KeyUp(key));
        }


        [SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods",
            Justification = "First parameter in extension cannot be null.")]
        public static TOut GetAutomationPropertyValue<TOut>(
            this WindowAppElement windowAppElement,
            AutomationProperty property)
        {
            try
            {
                return windowAppElement.Instance.GetPropertyValue<TOut>(property);
            }
            catch (NotSupportedException)
            {
                var msg = string.Format("Element '{0}' not support '{1}'", windowAppElement, property.ProgrammaticName);
                CruciatusFactory.Logger.Error(msg);

                throw new CruciatusException("GET PROPERTY VALUE FAILED");
            }
            catch (InvalidCastException invalidCastException)
            {
                var msg = string.Format("Invalid cast from '{0}' to '{1}'.", invalidCastException.Message,
                    typeof(TOut));
                CruciatusFactory.Logger.Error(msg);

                throw new CruciatusException("GET PROPERTY VALUE FAILED");
            }
        }


        public static T GetPattern<T>(this WindowAppElement element, AutomationPattern pattern) where T : class
        {
            return element.Instance.GetPattern<T>(pattern);
        }


        public static CheckBox ToCheckBox(this WindowAppElement element)
        {
            return new CheckBox(element);
        }


        public static ComboBox ToComboBox(this WindowAppElement element)
        {
            return new ComboBox(element);
        }


        public static DataGrid ToDataGrid(this WindowAppElement element)
        {
            return new DataGrid(element);
        }


        public static ListBox ToListBox(this WindowAppElement element)
        {
            return new ListBox(element);
        }


        public static Menu ToMenu(this WindowAppElement element)
        {
            return new Menu(element);
        }
    }
}