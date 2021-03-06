using WindowsAppDriver.Extensions;
using CommonLib;
using EngineLib.Elements;
using EngineLib.Exceptions;
using EngineLib.Extensions;

namespace WindowsAppDriver.CommandExecutors
{

    internal class ScrollToComboBoxItemExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var dataGridKey = this.ExecutedCommand.Parameters["ID"].ToString();
            var searchStrategy = this.ExecutedCommand.Parameters["using"].ToString();
            var searchValue = this.ExecutedCommand.Parameters["value"].ToString();

            var strategy = ByHelper.GetStrategy(searchStrategy, searchValue);

            var comboBox = this.Automators.ElementsRegistry.GetRegisteredElement(dataGridKey).ToComboBox();

            WindowAppElement element;
            try
            {
                element = comboBox.ScrollTo(strategy);
            }
            catch (CruciatusException exception)
            {
                return this.JsonResponse(ResponseStatus.NoSuchElement, exception);
            }

            var elementKey = this.Automators.ElementsRegistry.RegisterElement(element);

            return this.JsonResponse(ResponseStatus.Success, new JsonElementContent(elementKey));
        }

        #endregion
    }
}
