using apollo.Application.Common.Mappings;
using apollo.Domain.Entities.Core;
using apollo.Domain.Enums;
using AutoMapper;
using System.Linq;

namespace apollo.Application.Properties.Queries.DTOs
{
    public class PropertyDTO : IMapFrom<Property>
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int PropertyTypeID { get; set; }
        public int? GradeID { get; set; }
        public string GradeName { get; set; }
        public string PropertyTypeName { get; set; }
        public int? MasterPropertyID { get; set; }
        public string MasterPropertyName { get; set; }
        public int? ContactID { get; set; }
        public string ContactName { get; set; }
        public string ContactEmail { get; set; }
        public string ContactPhone { get; set; }
        public int? OwnerID { get; set; }
        public string OwnerName { get; set; }
        public string OwnerEmail { get; set; }
        public string OwnerPhone { get; set; }
        public int? OwnerCompanyID { get; set; }
        public string OwnerCompanyName { get; set; }
        public string MainImage { get; set; }
        public bool IsExclusive { get; set; }
        public string Note { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Property, PropertyDTO>()
                .ForMember(i => i.PropertyTypeName, o => o.MapFrom(m => m.PropertyType.Name))
                .ForMember(i => i.ContactName, o => o.MapFrom(m => m.Contact.FirstName + " " + m.Contact.LastName))
                .ForMember(i => i.ContactEmail, o => o.MapFrom(m => m.Contact.Email))
                .ForMember(i => i.ContactPhone, o => o.MapFrom(m => m.Contact.PhoneNumber))
                .ForMember(i => i.OwnerName, o => o.MapFrom(m => m.Owner.FirstName + " " + m.Owner.LastName))
                .ForMember(i => i.OwnerEmail, o => o.MapFrom(m => m.Owner.Email))
                .ForMember(i => i.OwnerPhone, o => o.MapFrom(m => m.Owner.PhoneNumber))
                .ForMember(i => i.OwnerCompanyName, o => o.MapFrom(m => m.OwnerCompany.Name))
                .ForMember(i => i.GradeName, o => o.MapFrom(m => m.Grade.Name))
                .ForMember(i => i.MasterPropertyName, o => o.MapFrom(m => m.MasterProperty.Name))
                .ForMember(i => i.MainImage, o => o.MapFrom(m => m.PropertyDocuments.FirstOrDefault(x => x.IsPrimary == true
                                        && x.IsDeleted == false
                                        && x.DocumentType == PropertyDocumentType.MainImage).Document.DocumentPath));
        }
    }
}
