using MediatR;

namespace apollo.Application.Agents.Commands.DeletePropertyAgent
{
    public class DeletePropertyAgentRequest : IRequest<bool>
    {
        public int PropertyID { get; set; }
        public int AgentID { get; set; }
    }
}
