using apollo.Application.Common.Exceptions;
using apollo.Application.Units.Queries.DTOs;
using AutoMapper;
using MediatR;
using Shared.Contracts;
using Shared.Extensions;
using System.Threading;
using System.Threading.Tasks;

namespace apollo.Application.Units.Queries.GetUnitByID
{
    public class GetUnitByIDQuery : IRequest<UnitDetailDTO>
    {
        public int Id { get; set; }
    }

    public class GetUnitByIDHandler : IRequestHandler<GetUnitByIDQuery, UnitDetailDTO>
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public GetUnitByIDHandler(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<UnitDetailDTO> Handle(GetUnitByIDQuery request, CancellationToken cancellationToken)
        {
            var unit = await _repository.UntrackFirstAsync<Domain.Entities.Core.Unit>(x => x.ID == request.Id && !x.IsDeleted,
                i => i.Include(x => x.Availability)
                .Include(x => x.ListingType)
                .Include(x => x.HandOverCondition)
                .Include(x => x.UnitStatus)
                .Include(x => x.Property.Agents).ThenInclude(x => x.Agent)
                .Include(x => x.Property.Contracts).ThenInclude(x => x.Company)
                .Include(x => x.Property.Contracts).ThenInclude(x => x.BrokerCompany)
                .Include(x => x.Property.Contracts).ThenInclude(x => x.Contact)
                .Include(x => x.Property.Contracts).ThenInclude(x => x.Broker)
                .Include(x => x.Property.Contracts).ThenInclude(x => x.TenantClassification)
                .Include(i => i.Property.PropertyDocuments)
                .ThenInclude(x => x.Document));
            if (unit == null)
                throw new NotFoundException();

            return _mapper.Map<UnitDetailDTO>(unit);
        }
    }
}