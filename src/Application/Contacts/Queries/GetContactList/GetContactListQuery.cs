using apollo.Application.Contacts.Queries.DTOs;
using apollo.Domain.Entities.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Contracts;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace apollo.Application.Contacts.Queries.GetContactList
{
    public class GetContactListQuery : IRequest<IEnumerable<ContactListDTO>>
    {
    }

    public class GetContactListQueryHandler : IRequestHandler<GetContactListQuery, IEnumerable<ContactListDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IRepository _repository;

        public GetContactListQueryHandler(IMapper mapper, IRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<IEnumerable<ContactListDTO>> Handle(GetContactListQuery request, CancellationToken cancellationToken)
        {
            var contacts = _repository.Fetch<Contact>(x => x.IsActive && !x.IsDeleted);
            return await contacts.ProjectTo<ContactListDTO>(_mapper.ConfigurationProvider).ToListAsync();
        }
    }
}
