using apollo.Application.Common.Interfaces;
using apollo.Application.Common.Models;
using apollo.Presentation.Controllers.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.DTO.Response;
using System;
using System.Threading.Tasks;

namespace apollo.Infrastructure.Controllers
{
    public class AzureController : APIBaseController
    {
        private readonly IAzureService _azureService;

        public AzureController(IAzureService azureService)
        {
            _azureService = azureService;
        }

        [HttpPost("blob/upload")]
        [ProducesResponseType(typeof(AzureBlobResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            var response = new Response<AzureBlobResult>();
            try
            {
                var result = await _azureService.UploadFile(file);
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
