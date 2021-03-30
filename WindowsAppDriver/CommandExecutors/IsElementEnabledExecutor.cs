using CommonLib;

namespace WindowsAppDriver.CommandExecutors
{
    #region using

    #endregion

    internal class IsElementEnabledExecutor : CommandExecutorBase
    {

        protected override string DoImpl()
        {
            var registeredKey = this.ExecutedCommand.Parameters["ID"].ToString();

            var element = this.Automators.ElementsRegistry.GetRegisteredElement(registeredKey);

            return this.JsonResponse(ResponseStatus.Success, element.Properties.IsEnabled);
        }

    }
}
