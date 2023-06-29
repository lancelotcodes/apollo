using apollo.Application.Common.Mappings;
using apollo.Domain.Entities.Core;
using apollo.Domain.Enums;
using AutoMapper;

namespace apollo.Application.Documents.Queries.DTO
{
    public class PropertyDocumentListDTO : IMapFrom<PropertyDocument>
    {
        public int ID { get; set; }
        public string DocumentName { get; set; }
        public long DocumentSize { get; set; }
        public string DocumentPath { get; set; }
        public bool IsPrimary { get; set; }
        public PropertyDocumentType? DocumentType { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<PropertyDocument, PropertyDocumentListDTO>()
                 .ForMember(i => i.ID, o => o.MapFrom(m => m.Document.ID))
                 .ForMember(i => i.DocumentName, o => o.MapFrom(m => m.Document.DocumentName))
                 .ForMember(i => i.DocumentPath, o => o.MapFrom(m => m.Document.DocumentPath))
                 .ForMember(i => i.DocumentSize, o => o.MapFrom(m => m.Document.DocumentSize));
        }
    }
}
