using System.Collections.Generic;
using WindowsAppDriver.Input;
using EngineLib;

namespace WindowsAppDriver.Automator
{
    internal class Automator
    {
        private static readonly object LockObject = new object();

        private static volatile Automator instance;

        public Automator(string session)
        {
            this.Session = session;
            this.ElementsRegistry = new ElementsRegistry();
        }

        public Capabilities ActualCapabilities { get; set; }

        public Application Application { get; set; }

        public ElementsRegistry ElementsRegistry { get; private set; }

        public string Session { get; private set; }

        public WiniumKeyboard WiniumKeyboard { get; set; }

        public static T GetValue<T>(IReadOnlyDictionary<string, object> parameters, string key) where T : class
        {
            object valueObject;
            parameters.TryGetValue(key, out valueObject);

            return valueObject as T;
        }

        public static Automator InstanceForSession(string sessionId)
        {
            if (instance == null)
            {
                lock (LockObject)
                {
                    if (instance == null)
                    {
                        if (sessionId == null)
                        {
                            sessionId = "AwesomeSession";
                        }

                        // TODO: Add actual support for sessions. Temporary return single Automator for any season
                        instance = new Automator(sessionId);
                    }
                }
            }

            return instance;
        }
    }
}