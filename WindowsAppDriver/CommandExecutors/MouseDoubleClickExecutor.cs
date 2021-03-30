using EngineLib;
using EngineLib.Core;

namespace WindowsAppDriver.CommandExecutors
{
    internal class MouseDoubleClickExecutor : CommandExecutorBase
    {
        protected override string DoImpl()
        {
            CruciatusFactory.Mouse.DoubleClick(MouseButton.Left);
            return this.JsonResponse();
        }
    }
}