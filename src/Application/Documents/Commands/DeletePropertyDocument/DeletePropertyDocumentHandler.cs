using apollo.Application.Common.Exceptions;
using apollo.Domain.Entities.Core;
using MediatR;
using Shared.Contracts;
using Shared.Extensions;
using System.Threading;
using System.Threading.Tasks;

namespace apollo.Application.Documents.Commands.DeletePropertyDocument
{
    public class DeletePropertyDocumentHandler : IRequestHandler<DeletePropertyDocumentRequest, bool>
    {
        private readonly IRepository _repository;

        public DeletePropertyDocumentHandler(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(DeletePropertyDocumentRequest request, CancellationToken cancellationToken)
        {
            var thumbnail = await _repository.FirstAsync<PropertyDocument>(x => x.Document.ThumbNailId == request.DocumentID, x => x.Include(x => x.Document).ThenInclude(x => x.ThumbNail));

            if (thumbnail != null && thumbnail.Document != null && thumbnail.Document.ThumbNail != null)
            {
                thumbnail.Document.ThumbNailId = null;
                _repository.HardDelete(thumbnail.Document.ThumbNail);
                return await _repository.SaveChangesAsync() > 0;
            }

            var propertyDocument = await _repository.UntrackFirstAsync<PropertyDocument>(x => x.DocumentID == request.DocumentID);
            var document = _repository.Get<Document>(x => x.ID == request.DocumentID);

            if (document == null)
            {
                throw new NotFoundException("Property document not found.");
            }
            if (propertyDocument != null)
            {
                _repository.HardDelete(propertyDocument);
            }
            _repository.HardDelete(document);
            var result = await _repository.SaveChangesAsync();
            return result > 0;
        }
    }
}
