using System.Collections.Generic;
using System.Linq;
using System.Windows.Automation;
using CommonLib;
using CommonLib.Exceptions;
using EngineLib.Core;
using EngineLib.Elements;
using EngineLib.Extensions;
using Newtonsoft.Json.Linq;

namespace WindowsAppDriver.CommandExecutors
{
    #region using

    #endregion

    internal class ExecuteScriptExecutor : CommandExecutorBase
    {
        #region Constants

        internal const string HelpArgumentsErrorMsg = "Arguments error. ";

        internal const string HelpUnknownScriptMsg = "Unknown script command '{0} {1}'. ";

        // internal const string HelpUrlAutomationScript =
        //     "https://github.com/2gis/Winium.Desktop/wiki/Command-Execute-Script#use-ui-automation-patterns-on-element";
        //
        // internal const string HelpUrlInputScript =
        //     "https://github.com/2gis/Winium.Desktop/wiki/Command-Execute-Script#simulate-input";
        //
        // internal const string HelpUrlScript = "https://github.com/2gis/Winium.Desktop/wiki/Command-Execute-Script";

        #endregion

        #region Methods

        protected override string DoImpl()
        {
            var script = this.ExecutedCommand.Parameters["script"].ToString();

            var prefix = string.Empty;
            string command;

            var index = script.IndexOf(':');
            if (index == -1)
            {
                command = script;
            }
            else
            {
                prefix = script.Substring(0, index);
                command = script.Substring(++index).Trim();
            }

            switch (prefix)
            {
                case "input":
                    this.ExecuteInputScript(command);
                    break;
                case "automation":
                    this.ExecuteAutomationScript(command);
                    break;
                default:
                    var msg = string.Format(HelpUnknownScriptMsg, prefix, command);
                    throw new AutomationException(msg, ResponseStatus.JavaScriptError);
            }

            return this.JsonResponse();
        }

        private void ExecuteAutomationScript(string command)
        {
            var args = (JArray)this.ExecutedCommand.Parameters["args"];
            var elementId = args[0]["ELEMENT"].ToString();

            var element = this.Automators.ElementsRegistry.GetRegisteredElement(elementId);

            switch (command)
            {
                case "ValuePattern.SetValue":
                    this.ValuePatternSetValue(element, args);
                    break;
                default:
                    var msg = string.Format(HelpUnknownScriptMsg, "automation:", command);
                    throw new AutomationException(msg, ResponseStatus.JavaScriptError);
            }
        }

        private void ExecuteInputScript(string command)
        {
            var args = (JArray)this.ExecutedCommand.Parameters["args"];
            var elementId = args[0]["ELEMENT"].ToString();

            var element = this.Automators.ElementsRegistry.GetRegisteredElement(elementId);

            switch (command)
            {
                case "ctrl_click":
                    element.ClickWithPressedCtrl();
                    return;
                case "brc_click":
                    element.Click(MouseButton.Left, ClickStrategies.BoundingRectangleCenter);
                    return;
                default:
                    var msg = string.Format(HelpUnknownScriptMsg, "input:", command);
                    throw new AutomationException(msg, ResponseStatus.JavaScriptError);
            }
        }

        private void ValuePatternSetValue(WindowAppElement element, IEnumerable<JToken> args)
        {
            var value = args.ElementAtOrDefault(1);
            if (value == null)
            {
                var msg = string.Format(HelpArgumentsErrorMsg);
                throw new AutomationException(msg, ResponseStatus.JavaScriptError);
            }

            element.GetPattern<ValuePattern>(ValuePattern.Pattern).SetValue(value.ToString());
        }

        #endregion
    }
}
