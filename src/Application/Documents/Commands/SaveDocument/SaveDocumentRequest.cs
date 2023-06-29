using apollo.Application.Documents.Queries.DTO;
using MediatR;
using System.Collections.Generic;

namespace apollo.Application.Documents.Commands.SaveDocument
{

    public class SaveDocumentRequest : IRequest<IEnumerable<DocumentDTO>>
    {
        public List<DocumentRequest> Documents { get; set; }
    }
    public class DocumentRequest
    {
        public string Filename { get; set; }
        public string Handle { get; set; }
        public string Mimetype { get; set; }
        public int Size { get; set; }
        public string Source { get; set; }
        public string Url { get; set; }
        public string UploadId { get; set; }
    }
}
