using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using EngineLib.Core;

namespace EngineLib.Extensions
{
    public static class IScreenshoterExtension
    {
        public static void AutomaticScreenshotCaptureIfNeeded(this IScreenshoter screenshoter)
        {
            if (CruciatusFactory.Settings.AutomaticScreenshotCapture)
            {
                screenshoter.TakeScreenshot();
            }
        }


        [SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0",
            Justification = "Main argument in extension method cannot be null")]
        public static void TakeScreenshot(this IScreenshoter screenshoter)
        {
            var timeStamp = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss-fff");
            var screenshotPath = Path.Combine(CruciatusFactory.Settings.ScreenshotsPath, timeStamp + ".png");
            screenshoter.GetScreenshot().SaveAsFile(screenshotPath);
            CruciatusFactory.Logger.Info("Saved screenshot to '{0}' file.", Path.GetFullPath(screenshotPath));
        }
    }
}