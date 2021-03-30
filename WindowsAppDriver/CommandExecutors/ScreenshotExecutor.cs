using CommonLib;
using EngineLib;

namespace WindowsAppDriver.CommandExecutors
{
    internal class ScreenshotExecutor : CommandExecutorBase
    {
        protected override string DoImpl()
        {
            var screenshot = CruciatusFactory.Screenshoter.GetScreenshot();
            var screenshotSource = screenshot.AsBase64String();

            return this.JsonResponse(ResponseStatus.Success, screenshotSource);
        }
    }
}