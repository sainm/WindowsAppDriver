using System;
using Newtonsoft.Json;

namespace WindowsAppDriver.CommandHelpers
{
    // ReSharper disable once InconsistentNaming
    public class OSInfo
    {
        private static string architecture;

        private static string version;


        [JsonProperty("arch")]
        public string Architecture
        {
            get { return architecture ?? (architecture = Environment.Is64BitOperatingSystem ? "x64" : "x86"); }
        }

        [JsonProperty("name")]
        public string Name
        {
            get { return "windows"; }
        }

        [JsonProperty("version")]
        public string Version
        {
            get { return version ?? (version = Environment.OSVersion.VersionString); }
        }
    }
}