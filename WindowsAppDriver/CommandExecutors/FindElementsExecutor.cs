using System.Linq;
using WindowsAppDriver.Extensions;
using CommonLib;
using EngineLib;

namespace WindowsAppDriver.CommandExecutors
{


    internal class FindElementsExecutor : CommandExecutorBase
    {
      

        protected override string DoImpl()
        {
            var searchValue = this.ExecutedCommand.Parameters["value"].ToString();
            var searchStrategy = this.ExecutedCommand.Parameters["using"].ToString();

            var strategy = ByHelper.GetStrategy(searchStrategy, searchValue);
            var elements = CruciatusFactory.Root.FindElements(strategy);

            var registeredKeys = this.Automators.ElementsRegistry.RegisterElements(elements);
            var registeredObjects = registeredKeys.Select(e => new JsonElementContent(e));
            return this.JsonResponse(ResponseStatus.Success, registeredObjects);
        }

      
    }
}
