using apollo.Application.Common.Interfaces;
using apollo.Application.Common.Models;
using apollo.Presentation.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.DTO.Response;
using System;
using System.Threading.Tasks;

namespace apollo.Infrastructure.Controllers
{
    public class GoogleController : APIBaseController
    {
        private readonly IGoogleServices _googleService;

        public GoogleController(IGoogleServices googleService)
        {
            _googleService = googleService;
        }

        [Authorize(Roles = "Superadmin,Property Manager")]
        [HttpGet("geocode")]
        public async Task<IActionResult> GetGeoCodeHttp(string address)
        {
            var response = new Response<GoogleGeoCodeResults>();

            try
            {
                var results = await _googleService.GetLocationCodingHttpClient(address);
                response.Data = results;
            }
            catch (Exception ex)
            {
                response.originalException = ex;
            }

            return HandleResponse(response);
        }

    }
}
