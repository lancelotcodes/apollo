using MediatR;

namespace apollo.Application.Documents.Commands.SavePropertyVideo
{
    public class SavePropertyVideoRequest : IRequest<bool>
    {
        public int ID { get; set; }
        public int PropertyID { get; set; }
        public string DocumentName { get; set; }
        public string DocumentPath { get; set; }
        public int ThumbNailId { get; set; }
    }
}
