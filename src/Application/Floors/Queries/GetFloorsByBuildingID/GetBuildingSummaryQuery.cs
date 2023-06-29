using apollo.Application.Floors.Queries.DTOs;
using apollo.Domain.Entities.Core;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Constants;
using Shared.Contracts;
using Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace apollo.Application.Floors.Queries.GetFloorsByBuildingID
{

    public class GetBuildingSummaryQuery : IRequest<IEnumerable<BuildingSummaryDTO>>
    {
        public int Id { get; set; }
    }

    public class GetBuildingSummaryHandler : IRequestHandler<GetBuildingSummaryQuery, IEnumerable<BuildingSummaryDTO>>
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public GetBuildingSummaryHandler(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BuildingSummaryDTO>> Handle(GetBuildingSummaryQuery request, CancellationToken cancellationToken)
        {
            DateTime now = DateTime.UtcNow;
            var response = new List<BuildingSummaryDTO>();
            var data = await _repository.Fetch<Floor>(x => x.BuildingID == request.Id && !x.IsDeleted,
                            i => i.Include(i => i.Units).ThenInclude(x => x.Property)
                            .ThenInclude(x => x.Contracts)
                            .Include(x => x.Units).ThenInclude(x => x.UnitStatus)).ToListAsync(cancellationToken);
            if (data != null && data.Count > 0)
            {
                var units = data.SelectMany(x => x.Units);
                if (units.Any())
                {
                    foreach (var unit in units)
                    {
                        if (unit.UnitStatus.Name == AppUnitStatuses.Tenanted)
                        {
                            var current = unit.Property.Contracts.OrderByDescending(c => c.EndDate).ThenByDescending(c => c.ID).FirstOrDefault(u => !u.IsHistorical);

                            if (current != null && current.EndDate.HasValue)
                            {
                                var existing = response.FirstOrDefault(x => x.ExpiryYear == current.EndDate.Value.Year.ToString());
                                if (existing == null)
                                {
                                    response.Add(new BuildingSummaryDTO { ExpiryYear = current.EndDate.Value.Year.ToString(), LeaseArea = unit.LeaseFloorArea });
                                }
                                else
                                {
                                    existing.LeaseArea += unit.LeaseFloorArea;
                                }
                            }
                            else
                            {
                                var existing = response.FirstOrDefault(x => x.ExpiryYear == "NotVerified");
                                if (existing == null)
                                {
                                    response.Add(new BuildingSummaryDTO { ExpiryYear = "NotVerified", LeaseArea = unit.LeaseFloorArea });
                                }
                                else
                                {
                                    existing.LeaseArea += unit.LeaseFloorArea;
                                }
                            }
                        }

                        else if (unit.UnitStatus.Name == AppUnitStatuses.Vacant)
                        {
                            var existing = response.FirstOrDefault(x => x.ExpiryYear == "Vacant");
                            if (existing == null)
                            {
                                response.Add(new BuildingSummaryDTO { ExpiryYear = "Vacant", LeaseArea = unit.LeaseFloorArea });
                            }
                            else
                            {
                                existing.LeaseArea += unit.LeaseFloorArea;
                            }
                        }
                    }

                    response.ForEach(x => x.LeaseArea = Math.Round(x.LeaseArea));
                }
            }

            return response;
        }
    }
}