using System.Diagnostics.CodeAnalysis;

namespace EngineLib.Core
{
    public interface IScreenshoter
    {
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "Reviewed.")]
        Screenshot GetScreenshot();
    }
}