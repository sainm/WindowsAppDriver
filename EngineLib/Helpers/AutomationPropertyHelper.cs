using System.Text.RegularExpressions;
using System.Windows.Automation;

namespace EngineLib.Helpers
{
    internal static class AutomationPropertyHelper
    {
        internal static string GetPropertyName(AutomationIdentifier property)
        {
            var pattern = new Regex(@".*\.(?<name>.*)Property");
            return pattern.Match(property.ProgrammaticName).Groups["name"].Value;
        }
    }
}