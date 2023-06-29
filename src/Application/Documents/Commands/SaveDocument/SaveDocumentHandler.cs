using apollo.Application.Documents.Queries.DTO;
using apollo.Domain.Entities.Core;
using AutoMapper;
using MediatR;
using Shared.Contracts;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace apollo.Application.Documents.Commands.SaveDocument
{
    public class SaveDocumentHandler : IRequestHandler<SaveDocumentRequest, IEnumerable<DocumentDTO>>
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public SaveDocumentHandler(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DocumentDTO>> Handle(SaveDocumentRequest request, CancellationToken cancellationToken)
        {
            if (request.Documents != null && request.Documents.Any())
            {
                var documents = new List<Document>();
                foreach (var x in request.Documents)
                {
                    if (_repository.Get<Document>(y => y.DocumentKey == x.Handle) == null)
                    {
                        documents.Add(new Document
                        {
                            DocumentName = x.Filename,
                            DocumentKey = x.Handle,
                            DocumentPath = x.Url,
                            DocumentSize = x.Size,
                            IsDeleted = false,
                            SourceType = DocSourceType.Filestack,
                        });
                    }
                }
                _repository.Save(documents);
                await _repository.SaveChangesAsync();
                return _mapper.Map<IEnumerable<DocumentDTO>>(documents);
            }

            return new List<DocumentDTO>();
        }
    }

}
