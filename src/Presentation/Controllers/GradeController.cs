using apollo.Application.Grades.DTOs;
using apollo.Application.Grades.GetGrades;
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
    public class GradeController : APIBaseController
    {

        [Authorize(Roles = "Superadmin,Property Manager")]
        [HttpGet("list/{PropertyTypeID}")]
        [ProducesResponseType(typeof(IEnumerable<ShortGradeListDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetGradesByPropertyTypeId([FromRoute] GetShortGradeQuery query)
        {
            var response = new Response<IEnumerable<ShortGradeListDTO>>();

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
