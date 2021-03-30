using System;
using CommonLib;
using EngineLib;
using EngineLib.Core;

namespace WindowsAppDriver.CommandExecutors
{
    internal class MouseClickExecutor : CommandExecutorBase
    {
        protected override string DoImpl()
        {
            var buttonId = Convert.ToInt32(this.ExecutedCommand.Parameters["button"]);

            switch ((MouseButton) buttonId)
            {
                case MouseButton.Left:
                    CruciatusFactory.Mouse.LeftButtonClick();
                    break;

                case MouseButton.Right:
                    CruciatusFactory.Mouse.RightButtonClick();
                    break;

                default:
                    return this.JsonResponse(ResponseStatus.UnknownCommand, "Mouse button behavior is not implemented");
            }

            return this.JsonResponse();
        }
    }
}