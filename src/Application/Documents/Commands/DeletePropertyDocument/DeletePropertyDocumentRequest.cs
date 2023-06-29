using MediatR;

namespace apollo.Application.Documents.Commands.DeletePropertyDocument
{
    public class DeletePropertyDocumentRequest : IRequest<bool>
    {
        public int DocumentID { get; set; }
    }
}
