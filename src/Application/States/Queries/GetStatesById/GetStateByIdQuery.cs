using apollo.Application.States.Queries.GetStates;
using MediatR;

namespace apollo.Application.States.Queries.GetStatesById
{
    public class GetStateByIdQuery : IRequest<StateDTO>
    {
        public int ID { get; set; }
    }
}
