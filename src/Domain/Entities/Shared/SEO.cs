using apollo.Domain.Enums;
using Shared.Domain.Common;
using System;

namespace apollo.Domain.Entities.Shared
{
    public class SEO : BaseEntityId
    {
        public int ID { get; set; }
        public string Url { get; set; }
        public string PageTitle { get; set; }
        public string PageDescription { get; set; }
        public string MetaKeyword { get; set; }
        public DateTimeOffset? PublishedDate { get; set; }
        public bool IsPublished { get; set; }
        public PublishType PublishType { get; set; }
        public bool IsFeatured { get; set; }
        public int FeaturedWeight { get; set; }
    }
}
