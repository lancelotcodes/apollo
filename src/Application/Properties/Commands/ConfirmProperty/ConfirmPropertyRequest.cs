using apollo.Application.Common.Models;
using MediatR;

namespace apollo.Application.Properties.Commands.ConfirmProperty
{
    public class ConfirmPropertyRequest : IRequest<SaveEntityResult>
    {
        public int PropertyID { get; set; }
    }
}
