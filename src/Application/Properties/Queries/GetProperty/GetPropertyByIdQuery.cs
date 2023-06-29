using apollo.Application.Common.Exceptions;
using apollo.Application.Properties.Queries.DTOs;
using apollo.Domain.Entities.Core;
using apollo.Domain.Enums;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Contracts;
using Shared.Extensions;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace apollo.Application.Properties.Queries.GetProperty
{
    public class GetPropertyByIdQuery : IRequest<PropertyDTO>
    {
        public int Id { get; set; }
    }

    public class GetPropertyHandler : IRequestHandler<GetPropertyByIdQuery, PropertyDTO>
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;
        public GetPropertyHandler(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<PropertyDTO> Handle(GetPropertyByIdQuery request, CancellationToken cancellationToken)
        {
            var property = await _repository.UntrackFirstAsync<Property>(x => x.ID == request.Id && !x.IsDeleted,
                              i => i.Include(i => i.Contact)
                               .Include(i => i.Owner)
                               .Include(i => i.OwnerCompany)
                               .Include(i => i.Grade)
                               .Include(i => i.PropertyType)
                               .Include(i => i.MasterProperty)
                                .Include(i => i.PropertyDocuments.Where(x => x.IsPrimary == true
                                        && x.IsDeleted == false
                                        && x.DocumentType == PropertyDocumentType.MainImage))
                               .ThenInclude(x => x.Document));

            if (property == null)
                throw new NotFoundException();

            return _mapper.Map<PropertyDTO>(property);
        }
    }
}
