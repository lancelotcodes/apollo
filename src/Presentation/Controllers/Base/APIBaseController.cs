using apollo.Application.Common.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shared.DTO.Response;
using System;
using System.Linq;

namespace apollo.Presentation.Controllers.Base
{
    /// <summary>
    /// APIBaseController class inhertits ControllerBase s serves as base class for all controllers.
    /// </summary>
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public abstract class APIBaseController : ControllerBase
    {
        private IMediator _mediator;

        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        #region helper methods
        public IActionResult HandleResponse<T>(IResponse<T> response)
        {

            if (response.originalException != null)
            {


                if (response.originalException is UnauthorizedAccessException)
                {
                    return Unauthorized(response);
                }

                else if (response.originalException is ValidationException exception)
                {
                    response.errors.AddRange(exception.Errors.Select(ve => new ValidationError { Name = ve.Key, Message = ve.Value.FirstOrDefault() }));
                }

                else if (response.originalException is NotFoundException)
                {
                    response.Success = true;
                    response.Data = default;
                }

                else
                {
                    response.Success = false;
                    response.Data = default;
                    response.Message = response.originalException.Message;
                }
            }

            if (response.Success && response.errors.Count == 0)
            {
                return Ok(response);
            }
            else
            {
                response.Success = false;
                return BadRequest(response);
            }
        }
        #endregion
    }
}