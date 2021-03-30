namespace WindowsAppDriver.CommandExecutors
{
    internal class ClickElementExecutor : CommandExecutorBase
    {
        protected override string DoImpl()
        {
            var registeredKey = this.ExecutedCommand.Parameters["ID"].ToString();
            this.Automators.ElementsRegistry.GetRegisteredElement(registeredKey).Click();

            return this.JsonResponse();
        }
    }
}