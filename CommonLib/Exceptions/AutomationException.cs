using System;

namespace CommonLib.Exceptions
{
    public class AutomationException : Exception
    {
        private ResponseStatus responseStatus = ResponseStatus.UnknownError;


        public AutomationException()
        {
        }

        public AutomationException(string message, ResponseStatus status)
            : base(message)
        {
            this.Status = status;
        }

        public AutomationException(string message, params object[] args)
            : base(string.Format(message, args))
        {
        }

        public AutomationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }


        public ResponseStatus Status
        {
            get { return this.responseStatus; }

            set { this.responseStatus = value; }
        }
    }
}