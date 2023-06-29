namespace apollo.Application.PropertyImages.Queries.GetImageLinkBySlug
{
    public class PropertyImageDTO
    {
        public int ID { get; set; }
        public string ImageLink { get; set; }
        public string Base64 { get; set; }
        public string Name { get; set; }
        public bool IsThumbNail { get; set; }
    }
}
