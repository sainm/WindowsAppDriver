using CommonLib;
using CommonLib.Exceptions;
using EngineLib.Extensions;

namespace WindowsAppDriver.CommandExecutors
{


    internal class FindMenuItemExecutor : CommandExecutorBase
    {
        

        protected override string DoImpl()
        {
            var dataGridKey = this.ExecutedCommand.Parameters["ID"].ToString();
            var headersPath = this.ExecutedCommand.Parameters["PATH"].ToString();

            var munu = this.Automators.ElementsRegistry.GetRegisteredElement(dataGridKey).ToMenu();

            var element = munu.GetItem(headersPath);
            if (element == null)
            {
                throw new AutomationException("No menu item was found", ResponseStatus.NoSuchElement);
            }

            var elementKey = this.Automators.ElementsRegistry.RegisterElement(element);

            return this.JsonResponse(ResponseStatus.Success, new JsonElementContent(elementKey));
        }

       
    }
}
