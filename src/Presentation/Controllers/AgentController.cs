using apollo.Application.Agents.Commands.DeletePropertyAgent;
using apollo.Application.Agents.Commands.SavePropertyAgent;
using apollo.Application.Contacts.Queries.DTOs;
using apollo.Application.Contacts.Queries.GetContactList;
using apollo.Presentation.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.DTO.Response;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace apollo.Presentation.Controllers
{
    public class AgentController : APIBaseController
    {

        [Authorize(Roles = "Superadmin,Property Manager")]
        [HttpGet("list")]
        [ProducesResponseType(typeof(IEnumerable<ContactListDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAgents([FromRoute] GetContactListQuery query)
        {
            var response = new Response<IEnumerable<ContactListDTO>>();

            try
            {
                var result = await Mediator.Send(query);
                response.Data = result;
            }
            catch (Exception ex)
            {
                response.originalException = ex;
            }

            return HandleResponse(response);
        }

        [Authorize(Roles = "Superadmin,Property Manager")]
        [HttpPost("save")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> SavePropertyAgents([FromBody] SavePropertyAgentRequest request)
        {
            var response = new Response<bool>();

            try
            {
                var result = await Mediator.Send(request);
                response.Data = result;
                response.Message = "Agents saved successfully.";
            }
            catch (Exception ex)
            {
                response.originalException = ex;
                response.Message = "Error occured while saving the agents.";
            }

            return HandleResponse(response);
        }

        [Authorize(Roles = "Superadmin,Property Manager")]
        [HttpDelete("delete")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeletePropertyAgents([FromBody] DeletePropertyAgentRequest request)
        {
            var response = new Response<bool>();

            try
            {
                var result = await Mediator.Send(request);
                response.Data = result;
                response.Message = "Agent deleted successfully.";
            }
            catch (Exception ex)
            {
                response.originalException = ex;
                response.Message = "Error occured while deleting the agents.";
            }

            return HandleResponse(response);
        }

    }
}
