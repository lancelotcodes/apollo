using apollo.Application.Common.Exceptions;
using apollo.Domain.Entities.Core;
using apollo.Domain.Enums;
using MediatR;
using Shared.Contracts;
using Shared.Extensions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace apollo.Application.Documents.Commands.SavePropertyVideo
{
    public class SavePropertyVideoHandler : IRequestHandler<SavePropertyVideoRequest, bool>
    {
        private readonly IRepository _repository;

        public SavePropertyVideoHandler(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(SavePropertyVideoRequest request, CancellationToken cancellationToken)
        {
            var property = _repository.Get<Property>(x => x.ID == request.PropertyID);

            if (property == null || property.IsDeleted)
            {
                throw new NotFoundException("Property not found.");
            }

            var existingPropertyDocument = _repository.First<PropertyDocument>(x => x.DocumentID == request.ID && x.DocumentType == PropertyDocumentType.ThreeSixtyVideo, i => i.Include(y => y.Document));
            if (existingPropertyDocument != null)
            {
                existingPropertyDocument.Document.ThumbNailId = request.ThumbNailId;
                existingPropertyDocument.Document.DocumentName = request.DocumentName;
                existingPropertyDocument.Document.DocumentPath = request.DocumentPath;
                var saveResult = await _repository.SaveChangesAsync();
                return saveResult > 0;
            }

            var propertyDocument = new PropertyDocument
            {
                PropertyID = request.PropertyID,
                Document = new Document
                {
                    DocumentKey = Guid.NewGuid().ToString(),
                    DocumentName = request.DocumentName,
                    ThumbNailId = request.ThumbNailId,
                    DocumentPath = request.DocumentPath,
                    IsDeleted = false,
                    SourceType = DocSourceType.External,
                },
                DocumentType = PropertyDocumentType.ThreeSixtyVideo
            };

            _repository.Save(propertyDocument);

            var result = await _repository.SaveChangesAsync();
            return result > 0;
        }
    }
}
