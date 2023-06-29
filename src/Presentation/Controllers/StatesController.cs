using apollo.Application.States.Commands.CreateState;
using apollo.Application.States.Commands.DeleteState;
using apollo.Application.States.Commands.UpdateState;
using apollo.Application.States.Queries.GetStates;
using apollo.Application.States.Queries.GetStatesById;
using apollo.Presentation.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.DTO.Response;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace apollo.Infrastructure.Controllers
{
    [Route("api")]
    public class StatesController : APIBaseController
    {
        [Authorize(Roles = "Superadmin,Property Manager")]
        [HttpGet("country/{countryId}/state")]
        public async Task<IActionResult> GetStateList(int countryId)
        {
            var response = new Response<IEnumerable<StateDTO>>();

            try
            {
                var result = await Mediator.Send(new GetStateQuery { CountryID = countryId });
                response.Data = result;
            }
            catch (Exception ex)
            {
                response.originalException = ex;
            }

            return HandleResponse(response);
        }

        [Authorize(Roles = "Superadmin,Property Manager")]
        [HttpGet("state/{id}")]
        public async Task<IActionResult> GetStateById(int id)
        {
            var response = new Response<StateDTO>();

            try
            {
                var result = await Mediator.Send(new GetStateByIdQuery
                {
                    ID = id
                });
                response.Data = result;
            }
            catch (Exception ex)
            {
                response.originalException = ex;
            }

            return HandleResponse(response);
        }

        [Authorize(Roles = "Superadmin,Property Manager")]
        [HttpPost("country/{countryId}/state")]
        public async Task<IActionResult> CreateState(int countryId, [FromBody] CreateStateCommand command)
        {
            var response = new Response<StateDTO>();

            try
            {
                command.CountryId = countryId;
                var create = await Mediator.Send(command);
                var result = await Mediator.Send(new GetStateByIdQuery { ID = create });
                response.Data = result;
            }
            catch (Exception ex)
            {
                response.originalException = ex;
            }

            return HandleResponse(response);
        }

        [Authorize(Roles = "Superadmin,Property Manager")]
        [HttpPut("state/{id}")]
        public async Task<IActionResult> UpdateState(int id, [FromBody] UpdateStateCommand command)
        {
            var response = new Response<StateDTO>();

            try
            {
                command.ID = id;
                var create = await Mediator.Send(command);
                var result = await Mediator.Send(new GetStateByIdQuery { ID = create });
                response.Data = result;
            }
            catch (Exception ex)
            {
                response.originalException = ex;
            }

            return HandleResponse(response);
        }

        [Authorize(Roles = "Superadmin,Property Manager")]
        [HttpDelete("state/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = new Response<object>();

            try
            {
                var result = await Mediator.Send(new DeleteStateCommand { ID = id });
            }
            catch (Exception ex)
            {
                response.originalException = ex;
            }

            return HandleResponse(response);
        }
    }
}
