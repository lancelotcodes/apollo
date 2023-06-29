using apollo.Application.Common.Exceptions;
using apollo.Application.Contacts.Commands.CreateContact;
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
    public class ContactController : APIBaseController
    {

        [Authorize(Roles = "Superadmin,Property Manager")]
        [HttpGet("list")]
        [ProducesResponseType(typeof(IEnumerable<ContactListDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetContacts([FromRoute] GetContactListQuery query)
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
        [HttpGet("{CompanyID}/list")]
        [ProducesResponseType(typeof(IEnumerable<ContactListDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetContactsByCompanyId([FromRoute] GetContactListByCompanyIDQuery query)
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
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        public async Task<IActionResult> SavePropertyContract([FromBody] CreateContactCommand request)
        {
            var response = new Response<int>();

            try
            {
                var result = await Mediator.Send(request);
                response.Data = result;
                response.Message = "Contact created successfully.";
            }
            catch (GenericException ex)
            {
                response.originalException = ex;
                response.Message = ex.Message;
            }
            catch (Exception ex)
            {
                response.originalException = ex;
                response.Message = "Error occured while saving the contract.";
            }

            return HandleResponse(response);
        }
    }
}
