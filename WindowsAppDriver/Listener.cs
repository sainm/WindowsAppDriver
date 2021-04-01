using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Sockets;
using CommonLib;

namespace WindowsAppDriver
{
    public class Listener
    {
        private static string _urnPrefix;
        private TcpListener _listener;
        private UriDispatchTables _dispatcher;
        private CommandExecutorDispatchTable _executorDispatcher;

        public int Port { get; private set; }

        public Listener(int listenerPort)
        {
            Port = listenerPort;
        }

        public static string UrnPrefix
        {
            get => _urnPrefix;

            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    // Normalize prefix
                    _urnPrefix = "/" + value.Trim('/');
                }
            }
        }

        public Uri Prefix { get; private set; }


        public void StartListening()
        {
            _listener = new TcpListener(IPAddress.Any, this.Port);
            Prefix = new Uri(string.Format(CultureInfo.InvariantCulture, "http://localhost:{0}", this.Port));
            _dispatcher = new UriDispatchTables(new Uri(this.Prefix, UrnPrefix));
            _executorDispatcher = new CommandExecutorDispatchTable();
            _listener.Start();
            while (true)
            {
                Logger.Debug("Waiting for a connection...");

                // Perform a blocking call to accept requests. 
                var client = _listener.AcceptTcpClient();
                using (var stream = client.GetStream())
                {
                    var acceptedRequest = HttpRequest.ReadFromStreamWithoutClosing(stream);

                    if (string.IsNullOrWhiteSpace(acceptedRequest.StartingLine))
                    {
                        Logger.Warn("ACCEPTED EMPTY REQUEST");
                    }
                    else
                    {
                        Logger.Debug("ACCEPTED REQUEST {0}", acceptedRequest.StartingLine);

                        var response = this.HandleRequest(acceptedRequest);
                        using (var writer = new StreamWriter(stream))
                        {
                            try
                            {
                                writer.Write(response);
                                writer.Flush();
                            }
                            catch (IOException ex)
                            {
                                Logger.Error("Error occured while writing response: {0}", ex);
                            }
                        }
                    }
                }
            }
        }

        private string HandleRequest(HttpRequest acceptedRequest)
        {
            var firstHeaderTokens = acceptedRequest.StartingLine.Split(' ');
            var method = firstHeaderTokens[0];
            var resourcePath = firstHeaderTokens[1];

            var uriToMatch = new Uri(Prefix, resourcePath);
            var matched = _dispatcher.Match(method, uriToMatch);

            if (matched == null)
            {
                Logger.Warn("Unknown command recived: {0}", uriToMatch);
                return HttpResponseHelper.ResponseString(HttpStatusCode.NotFound, "Unknown command " + uriToMatch);
            }

            var commandName = matched.Data.ToString();
            var commandToExecute = new Command(commandName, acceptedRequest.MessageBody);
            foreach (string variableName in matched.BoundVariables.Keys)
            {
                commandToExecute.Parameters[variableName] = matched.BoundVariables[variableName];
            }

            var commandResponse = this.ProcessCommand(commandToExecute);
            return HttpResponseHelper.ResponseString(commandResponse.HttpStatusCode, commandResponse.Content);
        }

        private CommandResponse ProcessCommand(Command command)
        {
            Logger.Info("COMMAND {0}\r\n{1}", command.Name, command.Parameters.ToString());
            var executor = this._executorDispatcher.GetExecutor(command.Name);
            executor.ExecutedCommand = command;
            var respnose = executor.Do();
            Logger.Debug("RESPONSE:\r\n{0}", respnose);

            return respnose;
        }
    }
}