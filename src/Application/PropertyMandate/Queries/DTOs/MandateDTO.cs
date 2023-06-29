using apollo.Application.Common.Mappings;
using apollo.Domain.Entities.Core;
using AutoMapper;
using System;

namespace apollo.Application.PropertyMandate.Queries.DTOs
{
    public class MandateDTO : IMapFrom<Mandate>
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int PropertyID { get; set; }
        public int AttachmentId { get; set; }
        public string AttachmentURL { get; set; }
        public string AttachmentName { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Mandate, MandateDTO>()
                .ForMember(x => x.AttachmentName, y => y.MapFrom(i => i.Attachment.DocumentName))
                .ForMember(x => x.AttachmentURL, y => y.MapFrom(i => i.Attachment.DocumentPath));
        }
    }
}
