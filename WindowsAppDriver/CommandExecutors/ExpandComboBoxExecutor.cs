using EngineLib.Extensions;

namespace WindowsAppDriver.CommandExecutors
{
    #region using

    #endregion

    internal class ExpandComboBoxExecutor : CommandExecutorBase
    {

        protected override string DoImpl()
        {
            var registeredKey = this.ExecutedCommand.Parameters["ID"].ToString();

            this.Automators.ElementsRegistry.GetRegisteredElement(registeredKey).ToComboBox().Expand();

            return this.JsonResponse();
        }

    }
}
