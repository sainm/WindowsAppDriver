using System.Windows.Automation;
using CommonLib;
using EngineLib.Exceptions;
using EngineLib.Extensions;

namespace WindowsAppDriver.CommandExecutors
{
    internal class IsElementSelectedExecutor : CommandExecutorBase
    {
        protected override string DoImpl()
        {
            var registeredKey = this.ExecutedCommand.Parameters["ID"].ToString();

            var element = this.Automators.ElementsRegistry.GetRegisteredElement(registeredKey);

            bool isSelected;

            try
            {
                var selectionItemProperty = SelectionItemPattern.IsSelectedProperty;
                isSelected = element.GetAutomationPropertyValue<bool>(selectionItemProperty);
            }
            catch (CruciatusException)
            {
                var toggleStateProperty = TogglePattern.ToggleStateProperty;
                var toggleState = element.GetAutomationPropertyValue<ToggleState>(toggleStateProperty);

                isSelected = toggleState == ToggleState.On;
            }

            return this.JsonResponse(ResponseStatus.Success, isSelected);
        }
    }
}