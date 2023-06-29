using apollo.Application.Buildings.Queries.DTOs;
using apollo.Application.Common.Exceptions;
using apollo.Domain.Entities.Core;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Contracts;
using Shared.Extensions;
using System.Threading;
using System.Threading.Tasks;

namespace apollo.Application.Buildings.Queries.GetPropertyBuilding
{
    public class GetBuildingByPropertyIdQuery : IRequest<PropertyBuildingDTO>
    {
        public int Id { get; set; }
    }

    public class GetBuildingByPropertyIdHandler : IRequestHandler<GetBuildingByPropertyIdQuery, PropertyBuildingDTO>
    {
        private readonly IMapper _mapper;
        private readonly IRepository _repository;

        public GetBuildingByPropertyIdHandler(IMapper mapper, IRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<PropertyBuildingDTO> Handle(GetBuildingByPropertyIdQuery request, CancellationToken cancellationToken)
        {
            var building = await _repository.FirstAsync<Building>(x => x.PropertyID == request.Id & !x.IsDeleted,
                i => i.Include(y => y.PropertyManagement).Include(i => i.ProjectStatus)
                      .Include(i => i.LeasingContact)
                      .Include(i => i.Developer)
                      .Include(i => i.OwnershipType));

            if (building == null)
                throw new NotFoundException();

            return _mapper.Map<PropertyBuildingDTO>(building);
        }
    }
}
