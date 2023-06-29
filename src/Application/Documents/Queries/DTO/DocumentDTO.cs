using apollo.Application.Common.Mappings;
using apollo.Domain.Entities.Core;

namespace apollo.Application.Documents.Queries.DTO
{
    public class DocumentDTO : IMapFrom<Document>
    {
        public int ID { get; set; }
        public string DocumentName { get; set; }
        public long DocumentSize { get; set; }
        public string DocumentPath { get; set; }
    }
}
