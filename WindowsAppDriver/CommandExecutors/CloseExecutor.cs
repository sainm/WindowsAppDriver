namespace WindowsAppDriver.CommandExecutors
{
    internal class CloseExecutor : CommandExecutorBase
    {

        protected override string DoImpl()
        {
            if (!this.Automators.ActualCapabilities.DebugConnectToRunningApp)
            {
                if (!this.Automators.Application.Close())
                {
                    this.Automators.Application.Kill();
                }

                this.Automators.ElementsRegistry.Clear();
            }

            return this.JsonResponse();
        }

    }
}
