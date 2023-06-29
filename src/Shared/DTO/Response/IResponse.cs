using System;
using System.Collections.Generic;

namespace Shared.DTO.Response
{
    public interface IResponse<T>
    {
        public bool Success { get; set; }
        public T Data { get; set; }
        public string Message { get; set; }
        public List<ValidationError> errors { get; set; }
        public Exception originalException { get; set; }
    }
}