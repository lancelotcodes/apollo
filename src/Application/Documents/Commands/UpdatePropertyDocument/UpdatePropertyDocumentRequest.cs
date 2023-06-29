using MediatR;

namespace apollo.Application.Documents.Commands.UpdatePropertyDocument
{
    public class UpdatePropertyDocumentRequest : IRequest<bool>
    {
        public int DocumentID { get; set; }
        public string DocumentName { get; set; }
    }
}
