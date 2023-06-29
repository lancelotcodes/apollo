using apollo.Application.Documents.Commands.DeletePropertyDocument;
using apollo.Application.Documents.Commands.SaveDocument;
using apollo.Application.Documents.Commands.UpdatePropertyDocument;
using apollo.Application.Documents.Queries.DTO;
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
    public class DocumentController : APIBaseController
    {
        [Authorize(Roles = "Superadmin")]
        [HttpDelete("{DocumentID}")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeletePropertyDocument([FromRoute] DeletePropertyDocumentRequest query)
        {
            var response = new Response<bool>();

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


        [Authorize(Roles = "Superadmin")]
        [HttpPut("update")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdatePropertyDocument([FromBody] UpdatePropertyDocumentRequest query)
        {
            var response = new Response<bool>();

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

        [Authorize(Roles = "Superadmin")]
        [HttpPost("save")]
        [ProducesResponseType(typeof(IEnumerable<DocumentDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> SaveDocument([FromBody] SaveDocumentRequest query)
        {
            var response = new Response<IEnumerable<DocumentDTO>>();

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

    }
}
