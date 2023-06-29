using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Shared.DTO.Response;
using Shared.Exceptions;

namespace apollo.Presentation.Middlewares
{
    public sealed class GlobalExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<GlobalExceptionFilter> _logger;

        public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            var exception = context.Exception;
            _logger.LogError(exception, nameof(GlobalExceptionFilter));

            var response = new Response<object>();
            response.Success = false;
            switch (exception)
            {
                case BaseNotFoundException baseNotFoundException:
                    response.Message = baseNotFoundException.Message;
                    break;

                case ExceptionWithStatusCodeException exceptionWithStatusCode:
                    response.Message = exceptionWithStatusCode.Message;
                    break;

                case ValidationException validationException:
                    response.Message = validationException.Message;
                    break;

                default:
                    response.Message = "Error during request execution";
                    break;
            }

            var objectResult = new ObjectResult(response);
            context.Result = objectResult;
        }
    }
}