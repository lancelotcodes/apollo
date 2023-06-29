using apollo.Application.Common.Exceptions;
using apollo.Application.Documents.Queries.DTO;
using apollo.Domain.Entities.Core;
using apollo.Domain.Enums;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Contracts;
using Shared.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace apollo.Application.Documents.Queries
{
    public class GetPropertyDocumentsByIdQuery : IRequest<IEnumerable<PropertyDocumentListDTO>>
    {
        public int Id { get; set; }
    }

    public class GetPropertyDocumentsHandler : IRequestHandler<GetPropertyDocumentsByIdQuery, IEnumerable<PropertyDocumentListDTO>>
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;
        public GetPropertyDocumentsHandler(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PropertyDocumentListDTO>> Handle(GetPropertyDocumentsByIdQuery request, CancellationToken cancellationToken)
        {
            var property = await _repository.UntrackFirstAsync<Property>(x => x.ID == request.Id && !x.IsDeleted,
                            i => i.Include(i => i.PropertyDocuments.Where(x => !x.IsDeleted && x.DocumentType != PropertyDocumentType.ThreeSixtyVideo))
                               .ThenInclude(x => x.Document));

            if (property == null)
                throw new NotFoundException();

            return _mapper.Map<IEnumerable<PropertyDocumentListDTO>>(property.PropertyDocuments);
        }
    }
}
