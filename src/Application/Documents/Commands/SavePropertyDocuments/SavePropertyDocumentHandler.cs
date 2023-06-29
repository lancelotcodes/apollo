using apollo.Application.Common.Exceptions;
using apollo.Domain.Entities.Core;
using MediatR;
using Shared.Contracts;
using System.Threading;
using System.Threading.Tasks;

namespace apollo.Application.Documents.Commands.SavePropertyDocuments
{
    public class SavePropertyDocumentHandler : IRequestHandler<SavePropertyDocumentRequest, bool>
    {
        private readonly IRepository _repository;

        public SavePropertyDocumentHandler(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(SavePropertyDocumentRequest request, CancellationToken cancellationToken)
        {
            var property = _repository.Get<Property>(x => x.ID == request.PropertyID);

            if (property == null || property.IsDeleted)
            {
                throw new NotFoundException("Property not found.");
            }

            foreach (var document in request.Images)
            {
                var existing = _repository.Get<PropertyDocument>(x => x.DocumentID == document.ID && x.PropertyID == request.PropertyID);
                if (existing == null)
                {
                    property.PropertyDocuments.Add(new PropertyDocument { PropertyID = request.PropertyID, DocumentID = document.DocumentID, DocumentType = document.DocumentType, IsPrimary = document.IsPrimary });
                }
                else
                {
                    existing.IsPrimary = document.IsPrimary;
                }
            }

            var result = await _repository.SaveChangesAsync();
            return result > 0;
        }
    }
}
