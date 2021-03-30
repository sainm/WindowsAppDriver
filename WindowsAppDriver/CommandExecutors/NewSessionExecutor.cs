using System.Threading;
using WindowsAppDriver.Automator;
using WindowsAppDriver.Input;
using CommonLib;
using EngineLib;
using EngineLib.Settings;
using Newtonsoft.Json;

namespace WindowsAppDriver.CommandExecutors
{

    internal class NewSessionExecutor : CommandExecutorBase
    {

        protected override string DoImpl()
        {
            // It is easier to reparse desired capabilities as JSON instead of re-mapping keys to attributes and calling type conversions, 
            // so we will take possible one time performance hit by serializing Dictionary and deserializing it as Capabilities object
            var serializedCapability =
                JsonConvert.SerializeObject(this.ExecutedCommand.Parameters["desiredCapabilities"]);
            this.Automators.ActualCapabilities = Capabilities.CapabilitiesFromJsonString(serializedCapability);

            this.InitializeApplication(this.Automators.ActualCapabilities.DebugConnectToRunningApp);
            this.InitializeKeyboardEmulator(this.Automators.ActualCapabilities.KeyboardSimulator);

            // Gives sometime to load visuals (needed only in case of slow emulation)
            Thread.Sleep(this.Automators.ActualCapabilities.LaunchDelay);

            return this.JsonResponse(ResponseStatus.Success, this.Automators.ActualCapabilities);
        }

        private void InitializeApplication(bool debugDoNotDeploy = false)
        {
            var appPath = this.Automators.ActualCapabilities.App;
            var appArguments = this.Automators.ActualCapabilities.Arguments;

            this.Automators.Application = new Application(appPath);
            if (!debugDoNotDeploy)
            {
                this.Automators.Application.Start(appArguments);
            }
        }

        private void InitializeKeyboardEmulator(KeyboardSimulatorType keyboardSimulatorType)
        {
            this.Automators.WiniumKeyboard = new WiniumKeyboard(keyboardSimulatorType);

            Logger.Debug("Current keyboard simulator: {0}", keyboardSimulatorType);
        }

    }
}