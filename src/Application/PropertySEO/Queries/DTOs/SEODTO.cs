using apollo.Application.Common.Mappings;
using apollo.Domain.Entities.Core;
using apollo.Domain.Enums;
using AutoMapper;
using System;

namespace apollo.Application.PropertySEO.Queries.DTOs
{
    public class SEODTO : IMapFrom<Property>
    {
        public int ID { get; set; }
        public int PropertyID { get; set; }
        public string Url { get; set; }
        public string PageTitle { get; set; }
        public string PageDescription { get; set; }
        public string MetaKeyword { get; set; }
        public DateTimeOffset? PublishedDate { get; set; }
        public bool IsPublished { get; set; }
        public PublishType PublishType { get; set; }
        public string PublishTypeName { get; set; }
        public bool IsFeatured { get; set; }
        public int FeaturedWeight { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Property, SEODTO>()
                .ForMember(i => i.PropertyID, o => o.MapFrom(m => m.ID))
                .ForMember(i => i.ID, o => o.MapFrom(m => m.SEO.ID))
                .ForMember(i => i.Url, o => o.MapFrom(m => m.SEO.Url))
                .ForMember(i => i.PageTitle, o => o.MapFrom(m => m.SEO.PageTitle))
                .ForMember(i => i.PageDescription, o => o.MapFrom(m => m.SEO.PageDescription))
                .ForMember(i => i.MetaKeyword, o => o.MapFrom(m => m.SEO.MetaKeyword))
                .ForMember(i => i.PublishedDate, o => o.MapFrom(m => m.SEO.PublishedDate))
                .ForMember(i => i.IsPublished, o => o.MapFrom(m => m.SEO.IsPublished))
                .ForMember(i => i.PublishType, o => o.MapFrom(m => m.SEO.PublishType))
                .ForMember(i => i.PublishTypeName, o => o.MapFrom(m => m.SEO.PublishType.ToString()))
                .ForMember(i => i.IsFeatured, o => o.MapFrom(m => m.SEO.IsFeatured))
                .ForMember(i => i.FeaturedWeight, o => o.MapFrom(m => m.SEO.FeaturedWeight));
        }
    }
}
