using CommonLib;
using EngineLib.Extensions;

namespace WindowsAppDriver.CommandExecutors
{

    internal class GetDataGridColumnCountExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var registeredKey = this.ExecutedCommand.Parameters["ID"].ToString();

            var dataGrid = this.Automators.ElementsRegistry.GetRegisteredElement(registeredKey).ToDataGrid();

            return this.JsonResponse(ResponseStatus.Success, dataGrid.ColumnCount);
        }

        #endregion
    }
}
