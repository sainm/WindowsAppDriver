using CommonLib;
using EngineLib;

namespace WindowsAppDriver.CommandExecutors
{

    internal class GetActiveElementExecutor : CommandExecutorBase
    {
        protected override string DoImpl()
        {
            var registeredKey = this.Automators.ElementsRegistry.RegisterElement(CruciatusFactory.FocusedElement);
            var registeredObject = new JsonElementContent(registeredKey);
            return this.JsonResponse(ResponseStatus.Success, registeredObject);
        }
    }
}