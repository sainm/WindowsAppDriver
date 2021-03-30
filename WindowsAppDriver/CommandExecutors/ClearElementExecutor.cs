namespace WindowsAppDriver.CommandExecutors
{
    internal class ClearElementExecutor : CommandExecutorBase
    {
        protected override string DoImpl()
        {
            var registeredKey = this.ExecutedCommand.Parameters["ID"].ToString();

            var element = this.Automators.ElementsRegistry.GetRegisteredElement(registeredKey);
            element.SetText(null);

            return this.JsonResponse();
        }
    }
}