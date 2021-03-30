using System.Linq;
using System.Windows.Automation;
using CommonLib;
using EngineLib;
using EngineLib.Core;
using EngineLib.Extensions;

namespace WindowsAppDriver.CommandExecutors
{
    internal class GetWindowHandlesExecutor : CommandExecutorBase
    {
        protected override string DoImpl()
        {
            var typeProperty = AutomationElement.ControlTypeProperty;
            var windows = CruciatusFactory.Root.FindElements(By.AutomationProperty(typeProperty, ControlType.Window));

            var handleProperty = AutomationElement.NativeWindowHandleProperty;

            var handles = windows.Select(element => element.GetAutomationPropertyValue<int>(handleProperty));

            return JsonResponse(ResponseStatus.Success, handles);
        }
    }
}