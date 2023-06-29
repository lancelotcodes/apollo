using apollo.Application.Common.Mappings;
using apollo.Application.Common.Models;
using apollo.Domain.Entities.Shared;
using AutoMapper;
using MediatR;

namespace apollo.Application.PropertySEO.Commands.SavePropertySEO
{
    public class SavePropertySEORequest : IRequest<SaveEntityResult>, IMapFrom<SEO>
    {
        public int ID { get; set; }
        public int PropertyID { get; set; }
        public string Url { get; set; }
        public string PageTitle { get; set; }
        public string PageDescription { get; set; }
        public string MetaKeyword { get; set; }
        public bool IsFeatured { get; set; }
        public int FeaturedWeight { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<SavePropertySEORequest, SEO>();
        }
    }
}
