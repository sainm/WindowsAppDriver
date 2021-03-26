namespace CommonLib
{
    public class CommandInfo
    {
        /// <summary>
        /// POST verb for the command info
        /// </summary>
        public const string PostCommand = "POST";

        /// <summary>
        /// GET verb for the command info
        /// </summary>
        public const string GetCommand = "GET";

        /// <summary>
        /// DELETE verb for the command info
        /// </summary>
        public const string DeleteCommand = "DELETE";

        public CommandInfo(string method, string resourcePath)
        {
            this.ResourcePath = resourcePath;
            this.Method = method;
        }

        public string Method { get; set; }

        public string ResourcePath { get; set; }
    }
}