using apollo.Application.Common.Exceptions;
using apollo.Domain.Entities.Core;
using MediatR;
using Shared.Contracts;
using System.Threading;
using System.Threading.Tasks;

namespace apollo.Application.Documents.Commands.UpdatePropertyDocument
{
    public class UpdatePropertyDocumentHandler : IRequestHandler<UpdatePropertyDocumentRequest, bool>
    {
        private readonly IRepository _repository;

        public UpdatePropertyDocumentHandler(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(UpdatePropertyDocumentRequest request, CancellationToken cancellationToken)
        {
            var document = _repository.Get<Document>(x => x.ID == request.DocumentID);

            if (document == null || document.IsDeleted)
            {
                throw new NotFoundException("Property document not found.");
            }

            document.DocumentName = request.DocumentName;

            var result = await _repository.SaveChangesAsync();
            return result > 0;
        }
    }
}
