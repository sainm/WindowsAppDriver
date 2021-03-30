using System;
using System.Windows.Automation;
using WindowsAppDriver.Extensions;
using CommonLib;
using EngineLib.Extensions;

namespace WindowsAppDriver.CommandExecutors
{
    internal class GetElementAttributeExecutor : CommandExecutorBase
    {
        protected override string DoImpl()
        {
            var registeredKey = this.ExecutedCommand.Parameters["ID"].ToString();
            var propertyName = this.ExecutedCommand.Parameters["NAME"].ToString();

            var element = this.Automators.ElementsRegistry.GetRegisteredElement(registeredKey);

            try
            {
                var property = AutomationPropertyHelper.GetAutomationProperty(propertyName);
                var propertyObject = element.GetAutomationPropertyValue<object>(property);

                return this.JsonResponse(ResponseStatus.Success, PrepareValueToSerialize(propertyObject));
            }
            catch (Exception)
            {
                return this.JsonResponse();
            }
        }

        private static object PrepareValueToSerialize(object obj)
        {
            if (obj == null)
            {
                return null;
            }

            if (obj.GetType().IsPrimitive)
            {
                return obj.ToString();
            }

            if (obj is ControlType controlType)
            {
                return controlType.ProgrammaticName;
            }

            return obj;
        }
    }
}