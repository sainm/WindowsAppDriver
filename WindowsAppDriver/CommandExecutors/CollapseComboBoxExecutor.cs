using EngineLib.Extensions;

namespace WindowsAppDriver.CommandExecutors
{
    #region using

    #endregion

    internal class CollapseComboBoxExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var registeredKey = this.ExecutedCommand.Parameters["ID"].ToString();

            this.Automators.ElementsRegistry.GetRegisteredElement(registeredKey).ToComboBox().Collapse();

            return this.JsonResponse();
        }

        #endregion
    }
}
