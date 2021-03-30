using CommonLib;
using EngineLib.Core;

namespace WindowsAppDriver.CommandExecutors
{
    
    internal class GetOrientationExecutor : CommandExecutorBase
    {

        protected override string DoImpl()
        {
            var orientation = RotationManager.GetCurrentOrientation();

            return this.JsonResponse(ResponseStatus.Success, orientation.ToString());
        }

    }
}