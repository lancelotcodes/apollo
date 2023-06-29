using System.Net;

namespace Shared.Exceptions
{
    public class BaseNotFoundException<T> : ExceptionWithStatusCodeException where T : class
    {
        public BaseNotFoundException() : this("The requested object of type " + typeof(T).Name + " could not be found")
        {

        }
        public BaseNotFoundException(string message) : base(message, HttpStatusCode.NotFound)
        {

        }
    }

    public class BaseNotFoundException : ExceptionWithStatusCodeException
    {
        public BaseNotFoundException() : this("The requested resource could not be found")
        {

        }
        public BaseNotFoundException(string message) : base(message, HttpStatusCode.NotFound)
        {

        }
    }
}