using System.Collections.Generic;
using WindowsAppDriver.CommandHelpers;
using CommonLib;

namespace WindowsAppDriver.CommandExecutors
{
    internal class StatusExecutor : CommandExecutorBase
    {
        protected override string DoImpl()
        {
            var response = new Dictionary<string, object> {{"build", new BuildInfo()}, {"os", new OSInfo()}};
            return this.JsonResponse(ResponseStatus.Success, response);
        }
    }
}