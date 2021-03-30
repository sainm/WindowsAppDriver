using System.Reflection;
using Newtonsoft.Json;

namespace WindowsAppDriver.CommandHelpers
{
    public class BuildInfo
    {
        private static string version;

        [JsonProperty("version")]
        public string Version
        {
            get { return version ?? (version = Assembly.GetExecutingAssembly().GetName().Version.ToString()); }
        }
    }
}