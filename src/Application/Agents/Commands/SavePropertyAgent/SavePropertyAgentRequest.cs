using apollo.Domain.Enums;
using MediatR;
using System.Collections.Generic;

namespace apollo.Application.Agents.Commands.SavePropertyAgent
{
    public class SavePropertyAgentRequest : IRequest<bool>
    {
        public int PropertyID { get; set; }
        public List<PropertyAgentRequest> Agents { get; set; }
    }

    public class PropertyAgentRequest
    {
        public int AgentID { get; set; }
        public bool IsVisibleOnWeb { get; set; }
        public AgentType AgentType { get; set; }
    }
}
