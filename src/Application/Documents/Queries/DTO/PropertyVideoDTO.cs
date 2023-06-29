using apollo.Application.Common.Mappings;
using apollo.Domain.Entities.Core;
using AutoMapper;

namespace apollo.Application.Documents.Queries.DTO
{
    public class PropertyVideoDTO : IMapFrom<PropertyDocument>
    {
        public int ID { get; set; }
        public int PropertyID { get; set; }
        public string DocumentName { get; set; }
        public string DocumentPath { get; set; }
        public int ThumbNailId { get; set; }
        public string ThumbNailName { get; set; }
        public string ThumbNailPath { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<PropertyDocument, PropertyVideoDTO>()
                .ForMember(x => x.ID, x => x.MapFrom(y => y.Document.ID))
                .ForMember(x => x.DocumentName, x => x.MapFrom(y => y.Document.DocumentName))
                .ForMember(x => x.DocumentPath, x => x.MapFrom(y => y.Document.DocumentPath))
                .ForMember(x => x.ThumbNailId, x => x.MapFrom(y => y.Document.ThumbNailId))
                .ForMember(x => x.ThumbNailName, x => x.MapFrom(y => y.Document.ThumbNail.DocumentName))
                .ForMember(x => x.ThumbNailPath, x => x.MapFrom(y => y.Document.ThumbNail.DocumentPath));
        }
    }
}
