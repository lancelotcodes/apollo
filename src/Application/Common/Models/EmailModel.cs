namespace apollo.Application.Common.Models
{
    public class EmailModel
    {
        public string To { get; set; }
        public string Copy { get; set; }
        public string Body { get; set; }
        public string Subject { get; set; }
    }

    public class EmailContentModel
    {
        public string FromEmail { get; set; }
        public string FromName { get; set; }
        public string ToEmail { get; set; }
        public string ToName { get; set; }
        public string TemplateId { get; set; }
        public object DynamicObject { get; set; }
    }

}
