using CommonLib;
using EngineLib.Elements;
using EngineLib.Exceptions;
using EngineLib.Extensions;

namespace WindowsAppDriver.CommandExecutors
{
    #region using

    #endregion

    internal class FindDataGridCellExecutor : CommandExecutorBase
    {

        protected override string DoImpl()
        {
            var dataGridKey = this.ExecutedCommand.Parameters["ID"].ToString();
            var column = int.Parse(this.ExecutedCommand.Parameters["COLUMN"].ToString());
            var row = int.Parse(this.ExecutedCommand.Parameters["ROW"].ToString());

            var dataGrid = this.Automators.ElementsRegistry.GetRegisteredElement(dataGridKey).ToDataGrid();

            WindowAppElement dataGridCell;
            try
            {
                dataGridCell = dataGrid.Item(row, column);
            }
            catch (CruciatusException exception)
            {
                return this.JsonResponse(ResponseStatus.NoSuchElement, exception);
            }

            var registeredKey = this.Automators.ElementsRegistry.RegisterElement(dataGridCell);
            var registeredObject = new JsonElementContent(registeredKey);

            return this.JsonResponse(ResponseStatus.Success, registeredObject);
        }

    }
}
