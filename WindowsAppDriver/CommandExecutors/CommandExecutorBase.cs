using System;
using System.Net;
using CommonLib;
using CommonLib.Exceptions;
using Newtonsoft.Json;

namespace WindowsAppDriver.CommandExecutors
{
    internal abstract class CommandExecutorBase
    {
        public Command ExecutedCommand { get; set; }


        protected Automator.Automator Automators { get; set; }


        public CommandResponse Do()
        {
            if (this.ExecutedCommand == null)
            {
                throw new NullReferenceException("ExecutedCommand property must be set before calling Do");
            }

            try
            {
                var session = this.ExecutedCommand.SessionId;
                this.Automators = Automator.Automator.InstanceForSession(session);
                return CommandResponse.Create(HttpStatusCode.OK, this.DoImpl());
            }
            catch (AutomationException exception)
            {
                return CommandResponse.Create(HttpStatusCode.OK, this.JsonResponse(exception.Status, exception));
            }
            catch (NotImplementedException exception)
            {
                return CommandResponse.Create(
                    HttpStatusCode.NotImplemented,
                    this.JsonResponse(ResponseStatus.UnknownCommand, exception));
            }
            catch (Exception exception)
            {
                return CommandResponse.Create(
                    HttpStatusCode.OK,
                    this.JsonResponse(ResponseStatus.UnknownError, exception));
            }
        }


        protected abstract string DoImpl();

        /// <summary>
        /// The JsonResponse with SUCCESS status and NULL value.
        /// </summary>
        protected string JsonResponse()
        {
            return this.JsonResponse(ResponseStatus.Success, null);
        }

        protected string JsonResponse(ResponseStatus status, object value)
        {
            return JsonConvert.SerializeObject(
                new JsonResponse(this.Automators.Session, status, value),
                Formatting.Indented);
        }
    }
}