using apollo.Application.Agents.Queries.DTOs;
using apollo.Application.Buildings.Queries.DTOs;
using apollo.Application.Common.Mappings;
using apollo.Application.PropertyAddress.Queries.DTOs;
using apollo.Application.Units.Queries.DTOs;
using apollo.Domain.Entities.Core;
using apollo.Domain.Enums;
using AutoMapper;
using Shared.Constants;
using System.Collections.Generic;
using System.Linq;

namespace apollo.Application.OfferGeneration.Queries.DTOs
{
    public class OfferOptionListDTO : IMapFrom<Property>
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int PropertyTypeID { get; set; }
        public int? GradeID { get; set; }
        public string GradeName { get; set; }
        public string PropertyTypeName { get; set; }
        public int? ContactID { get; set; }
        public string ContactName { get; set; }
        public string ContactEmail { get; set; }
        public string ContactPhone { get; set; }
        public string MainImage { get; set; }
        public int? AvailableUnits { get; set; }
        public decimal? AvailableSpace { get; set; }
        public BuildingShortDTO Building { get; set; }
        public AddressShortDTO Address { get; set; }
        public PropertyAgentDTO Agent { get; set; }
        public List<UnitDetailForOfferDTO> Units { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Property, OfferOptionListDTO>()
                .ForMember(i => i.Units, o => o.MapFrom(m => m.Building.Floors.SelectMany(x => x.Units)))
                .ForMember(i => i.Address, o => o.MapFrom(m => m.Address))
                .ForMember(i => i.Building, o => o.MapFrom(m => m.Building))
                .ForMember(i => i.Agent, o => o.MapFrom(m => m.Agents.FirstOrDefault(x => x.IsVisibleOnWeb == true || !x.IsDeleted)))
                .ForMember(i => i.PropertyTypeName, o => o.MapFrom(m => m.PropertyType.Name))
                .ForMember(i => i.ContactName, o => o.MapFrom(m => m.Contact.FirstName + " " + m.Contact.LastName))
                .ForMember(i => i.ContactEmail, o => o.MapFrom(m => m.Contact.Email))
                .ForMember(i => i.ContactPhone, o => o.MapFrom(m => m.Contact.PhoneNumber))
                .ForMember(i => i.GradeName, o => o.MapFrom(m => m.Grade.Name))
                .ForMember(i => i.MainImage, o => o.MapFrom(m => m.PropertyDocuments.FirstOrDefault(x => x.IsPrimary == true
                                        && x.IsDeleted == false
                                        && x.DocumentType == PropertyDocumentType.MainImage).Document.DocumentPath))
                .ForMember(i => i.AvailableUnits, o => o.MapFrom(m => m.Building.Floors.SelectMany(x => x.Units).Count(x => x.Availability.Name == AppUnitAvailability.Available)))
                .ForMember(i => i.AvailableSpace, o => o.MapFrom(m => m.Building.Floors.SelectMany(x => x.Units).Where(x => x.Availability.Name == AppUnitAvailability.Available).Sum(x => x.LeaseFloorArea)));
        }
    }
}
