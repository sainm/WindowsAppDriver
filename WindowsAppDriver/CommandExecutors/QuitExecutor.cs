using WindowsAppDriver.CommandExecutors;

namespace Winium.Desktop.Driver.CommandExecutors
{
    internal class QuitExecutor : CommandExecutorBase
    {
        #region Methods

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

        #endregion
    }
}
