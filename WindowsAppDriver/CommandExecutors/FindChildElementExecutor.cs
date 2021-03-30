using WindowsAppDriver.Extensions;
using CommonLib;
using CommonLib.Exceptions;

namespace WindowsAppDriver.CommandExecutors
{


    internal class FindChildElementExecutor : CommandExecutorBase
    {

        protected override string DoImpl()
        {
            var parentKey = this.ExecutedCommand.Parameters["ID"].ToString();
            var searchValue = this.ExecutedCommand.Parameters["value"].ToString();
            var searchStrategy = this.ExecutedCommand.Parameters["using"].ToString();

            var parent = this.Automators.ElementsRegistry.GetRegisteredElement(parentKey);
            var strategy = ByHelper.GetStrategy(searchStrategy, searchValue);
            var element = parent.FindElement(strategy);
            if (element == null)
            {
                throw new AutomationException("Element cannot be found", ResponseStatus.NoSuchElement);
            }

            var registeredKey = this.Automators.ElementsRegistry.RegisterElement(element);
            var registeredObject = new JsonElementContent(registeredKey);
            return this.JsonResponse(ResponseStatus.Success, registeredObject);
        }

    }
}