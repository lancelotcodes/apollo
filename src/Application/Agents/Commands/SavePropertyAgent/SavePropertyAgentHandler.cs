using apollo.Application.Common.Exceptions;
using apollo.Domain.Entities.Core;
using MediatR;
using Shared.Contracts;
using Shared.Extensions;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace apollo.Application.Agents.Commands.SavePropertyAgent
{
    public class SavePropertyAgentHandler : IRequestHandler<SavePropertyAgentRequest, bool>
    {
        private readonly IRepository _repository;

        public SavePropertyAgentHandler(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(SavePropertyAgentRequest request, CancellationToken cancellationToken)
        {
            var property = _repository.Get<Property>(x => x.ID == request.PropertyID, y => y.Include(i => i.Agents));

            if (property == null || property.IsDeleted)
            {
                throw new NotFoundException("Property not found.");
            }

            if (!property.Agents.Any())
            {
                request.Agents.ForEach(x => property.Agents.Add(new PropertyAgent { PropertyID = request.PropertyID, AgentID = x.AgentID, IsVisibleOnWeb = x.IsVisibleOnWeb, AgentType = x.AgentType }));
            }
            else
            {
                request.Agents.ForEach(x =>
                {
                    var propertyAgent = property.Agents.FirstOrDefault(y => y.AgentID == x.AgentID);
                    if (propertyAgent == null)
                    {
                        property.Agents.Add(new PropertyAgent { PropertyID = request.PropertyID, AgentID = x.AgentID, IsVisibleOnWeb = x.IsVisibleOnWeb, AgentType = x.AgentType });
                    }
                    else
                    {
                        propertyAgent.IsDeleted = false;
                        propertyAgent.IsVisibleOnWeb = x.IsVisibleOnWeb;
                        propertyAgent.AgentType = x.AgentType;
                    }
                });
            }

            var result = await _repository.SaveChangesAsync();
            return result > 0;
        }
    }
}
