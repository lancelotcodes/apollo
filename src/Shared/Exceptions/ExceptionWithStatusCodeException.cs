using System;
using System.Net;
using System.Runtime.Serialization;

namespace Shared.Exceptions
{
    [Serializable]
    public class ExceptionWithStatusCodeException : Exception
    {
        public ExceptionWithStatusCodeException(HttpStatusCode statusCode)
        {
            StatusCode = statusCode;
        }

        public ExceptionWithStatusCodeException(string message, HttpStatusCode statusCode) : base(message)
        {
            StatusCode = statusCode;
        }

        public HttpStatusCode StatusCode { get; private set; }

        protected ExceptionWithStatusCodeException(SerializationInfo serializationInfo, StreamingContext streamingContext)
            : base(serializationInfo, streamingContext)
        {
        }
    }
}