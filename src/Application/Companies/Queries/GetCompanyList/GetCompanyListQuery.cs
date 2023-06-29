using apollo.Application.Companies.Queries.DTOs;
using apollo.Domain.Entities.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Contracts;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace apollo.Application.Companies.Queries.GetCompanyList
{
    public class GetCompanyListQuery : IRequest<IEnumerable<CompanyListDTO>>
    {
    }

    public class GetCompanyListQueryHandler : IRequestHandler<GetCompanyListQuery, IEnumerable<CompanyListDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IRepository _repository;

        public GetCompanyListQueryHandler(IMapper mapper, IRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<IEnumerable<CompanyListDTO>> Handle(GetCompanyListQuery request, CancellationToken cancellationToken)
        {
            var Companys = _repository.Fetch<Company>(x => x.IsActive && !x.IsDeleted);
            return await Companys.ProjectTo<CompanyListDTO>(_mapper.ConfigurationProvider).ToListAsync();
        }
    }
}
