using apollo.Application.Countries.Commands.CreateCountry;
using apollo.Application.Countries.Commands.DeleteCountry;
using apollo.Application.Countries.Commands.UpdateCountry;
using apollo.Application.Countries.Queries.GetCountryById;
using apollo.Application.Countries.Queries.GetCountryList;
using apollo.Presentation.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.DTO.Response;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace apollo.Infrastructure.Controllers
{
    [Route("api/country")]
    public class CountriesController : APIBaseController
    {
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CountryDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCountryList()
        {
            var response = new Response<IEnumerable<CountryDTO>>();

            try
            {
                var result = await Mediator.Send(new GetCountryListQuery());
                response.Data = result;
            }
            catch (Exception ex)
            {
                response.originalException = ex;
            }

            return HandleResponse(response);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CountryDTO), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCountryById(int id)
        {
            var response = new Response<CountryDTO>();

            try
            {
                var result = await Mediator.Send(new GetCountryByIdQuery
                {
                    Id = id
                });
                response.Data = result;
            }
            catch (Exception ex)
            {
                response.originalException = ex;
            }

            return HandleResponse(response);
        }

        [Authorize(Roles = "Superadmin")]
        [HttpPost]
        [ProducesResponseType(typeof(CountryDTO), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateCountry([FromBody] AddCountryCommand command)
        {
            var response = new Response<CountryDTO>();

            try
            {
                var create = await Mediator.Send(command);
                var result = await Mediator.Send(new GetCountryByIdQuery { Id = create });
                response.Data = result;
            }
            catch (Exception ex)
            {
                response.originalException = ex;
            }

            return HandleResponse(response);
        }

        [Authorize(Roles = "Superadmin")]
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(CountryDTO), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateCountry(int id, [FromBody] UpdateCountryCommand command)
        {
            var response = new Response<CountryDTO>();

            try
            {
                command.Id = id;
                var create = await Mediator.Send(command);
                var result = await Mediator.Send(new GetCountryByIdQuery { Id = create });
                response.Data = result;
            }
            catch (Exception ex)
            {
                response.originalException = ex;
            }

            return HandleResponse(response);
        }

        [Authorize(Roles = "Superadmin")]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(int id)
        {
            var response = new Response<object>();

            try
            {
                var result = await Mediator.Send(new DeleteCountryCommand { Id = id });
                response.Data = result;
            }
            catch (Exception ex)
            {
                response.originalException = ex;
            }

            return HandleResponse(response);
        }

    }
}


