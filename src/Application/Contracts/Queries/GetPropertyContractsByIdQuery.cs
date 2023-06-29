using apollo.Application.Common.Exceptions;
using apollo.Application.Contracts.Queries.DTOs;
using apollo.Domain.Entities.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Contracts;
using Shared.Extensions;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace apollo.Application.Contracts.Queries
{
    public class GetPropertyContractsByIdQuery : IRequest<IEnumerable<ContractListDTO>>
    {
        public int Id { get; set; }
    }

    public class GetPropertyContractsHandler : IRequestHandler<GetPropertyContractsByIdQuery, IEnumerable<ContractListDTO>>
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;
        public GetPropertyContractsHandler(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ContractListDTO>> Handle(GetPropertyContractsByIdQuery request, CancellationToken cancellationToken)
        {
            var contracts = _repository.Fetch<Contract>(x => x.PropertyID == request.Id && !x.IsDeleted, i =>
            i.Include(i => i.Broker)
            .Include(i => i.BrokerCompany)
            .Include(i => i.Company)
            .Include(i => i.Contact)
            .Include(i => i.TenantClassification)
            );

            if (contracts == null)
                throw new NotFoundException();

            return await contracts.ProjectTo<ContractListDTO>(_mapper.ConfigurationProvider).ToListAsync();
        }
    }
}
