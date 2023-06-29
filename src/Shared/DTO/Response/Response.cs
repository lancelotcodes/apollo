using System;
using System.Collections.Generic;

namespace Shared.DTO.Response
{
    public class Response<T> : IResponse<T>
    {
        public Response()
        {
            Success = true;
            errors = new List<ValidationError>();
        }
        public bool Success { get; set; }
        public T Data { get; set; }
        public string Message { get; set; }
        public List<ValidationError> errors { get; set; }
        public Exception originalException { get; set; }
    }

    public class ValidationError
    {
        public string Name { get; set; }
        public string Message { get; set; }
    }
}