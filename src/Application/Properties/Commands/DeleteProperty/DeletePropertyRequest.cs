using MediatR;

namespace apollo.Application.Properties.Commands.DeleteProperty
{
    public class DeletePropertyRequest : IRequest<bool>
    {
        public int PropertyID { get; set; }
    }
}
