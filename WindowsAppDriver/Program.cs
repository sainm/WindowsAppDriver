using System;
using CommandLine;

namespace WindowsAppDriver
{
    internal class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            var port = 9999;
            Parser.Default.ParseArguments<CommandLineOptions>(args).WithParsed(o =>
            {
                if (o.Port.HasValue)
                    port = o.Port.Value;
                if (o.LogPath != null)
                    Logger.TargetFile(o.LogPath, o.Verbose);
                else if (!o.Silent)
                    Logger.TargetConsole(o.Verbose);
                else
                    Logger.TargetNull();
                var listener = new Listener(port);
                Listener.UrnPrefix = o.UrlBase;
                Console.WriteLine("Starting Windows Desktop Driver on port {0}\n", port);
                listener.StartListening();
            });
        }
    }
}