using apollo.Application.Common.Exceptions;
using apollo.Application.Companies.Commands.CreateCompany;
using apollo.Application.Companies.Queries.DTOs;
using apollo.Application.Companies.Queries.GetCompanyList;
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
    public class CompanyController : APIBaseController
    {

        [Authorize(Roles = "Superadmin,Property Manager")]
        [HttpGet("list")]
        [ProducesResponseType(typeof(IEnumerable<CompanyListDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCompanyList([FromRoute] GetCompanyListQuery query)
        {
            var response = new Response<IEnumerable<CompanyListDTO>>();

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
        public async Task<IActionResult> SavePropertyContract([FromBody] CreateCompanyCommand request)
        {
            var response = new Response<int>();

            try
            {
                var result = await Mediator.Send(request);
                response.Data = result;
                response.Message = "Company created successfully.";
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
