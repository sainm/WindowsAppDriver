using WindowsAppDriver.Extensions;
using CommonLib;
using CommonLib.Exceptions;
using EngineLib;

namespace WindowsAppDriver.CommandExecutors
{
    internal class FindElementExecutor : CommandExecutorBase
    {
        protected override string DoImpl()
        {
            var searchValue = this.ExecutedCommand.Parameters["value"].ToString();
            var searchStrategy = this.ExecutedCommand.Parameters["using"].ToString();

            var strategy = ByHelper.GetStrategy(searchStrategy, searchValue);
            var element = CruciatusFactory.Root.FindElement(strategy);
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