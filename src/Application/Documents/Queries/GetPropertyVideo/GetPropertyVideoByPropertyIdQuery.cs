using apollo.Application.Common.Exceptions;
using apollo.Application.Documents.Queries.DTO;
using apollo.Domain.Entities.Core;
using apollo.Domain.Enums;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Contracts;
using Shared.Extensions;
using System.Threading;
using System.Threading.Tasks;

namespace apollo.Application.Documents.Queries.GetPropertyVideo
{
    public class GetPropertyVideoByPropertyIdQuery : IRequest<PropertyVideoDTO>
    {
        public int Id { get; set; }
    }

    public class GetPropertyVideoByPropertyIdHandler : IRequestHandler<GetPropertyVideoByPropertyIdQuery, PropertyVideoDTO>
    {
        private readonly IMapper _mapper;
        private readonly IRepository _repository;

        public GetPropertyVideoByPropertyIdHandler(IMapper mapper, IRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<PropertyVideoDTO> Handle(GetPropertyVideoByPropertyIdQuery request, CancellationToken cancellationToken)
        {
            var document = await _repository.UntrackFirstAsync<PropertyDocument>(x => x.PropertyID == request.Id && x.DocumentType == PropertyDocumentType.ThreeSixtyVideo && !x.IsDeleted,
                i => i.Include(y => y.Document).ThenInclude(y => y.ThumbNail));

            if (document?.Document?.ThumbNail != null && document.Document.ThumbNail.IsDeleted)
            {
                document.Document.ThumbNail = null;
            }

            if (document == null)
                throw new NotFoundException();

            return _mapper.Map<PropertyVideoDTO>(document);
        }
    }
}
