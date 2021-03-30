using CommonLib;
using EngineLib.Extensions;

namespace WindowsAppDriver.CommandExecutors
{

    internal class IsComboBoxExpandedExecutor : CommandExecutorBase
    {

        protected override string DoImpl()
        {
            var registeredKey = this.ExecutedCommand.Parameters["ID"].ToString();

            var element = this.Automators.ElementsRegistry.GetRegisteredElement(registeredKey);

            return this.JsonResponse(ResponseStatus.Success, element.ToComboBox().IsExpanded);
        }

    }
}
