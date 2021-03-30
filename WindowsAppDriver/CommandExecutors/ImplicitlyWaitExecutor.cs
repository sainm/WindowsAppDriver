using System;
using EngineLib;

namespace WindowsAppDriver.CommandExecutors
{
    internal class ImplicitlyWaitExecutor : CommandExecutorBase
    {
        protected override string DoImpl()
        {
            var timeout = this.ExecutedCommand.Parameters["ms"];

            CruciatusFactory.Settings.SearchTimeout = Convert.ToInt32(timeout);

            return this.JsonResponse();
        }
    }
}