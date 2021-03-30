using System;
using System.Runtime.Serialization;

namespace EngineLib.Exceptions
{
    [Serializable]
    public class CruciatusException : Exception
    {
        public CruciatusException()
        {
        }

        public CruciatusException(string message)
            : base(message)
        {
        }


        public CruciatusException(string message, Exception innerException)
            : base(message, innerException)
        {
        }


        protected CruciatusException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}