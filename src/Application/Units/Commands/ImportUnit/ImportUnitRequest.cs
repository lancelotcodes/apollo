using apollo.Application.Floors.Queries.DTOs;
using apollo.Application.Units.Queries.DTOs;
using MediatR;
using System.Collections.Generic;

namespace apollo.Application.Units.Commands.CreateUnit
{
    public class ImportUnitRequest : IRequest<ImportStackingPlanDetailDTO>
    {
        public List<ExportStackingPlanDTO> Units { get; set; }

    }
}
