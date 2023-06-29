using apollo.Application.Common.Exceptions;
using apollo.Domain.Entities.Core;
using MediatR;
using Shared.Contracts;
using System.Threading;
using System.Threading.Tasks;

namespace apollo.Application.Agents.Commands.DeletePropertyAgent
{
    public class DeletePropertyAgentHandler : IRequestHandler<DeletePropertyAgentRequest, bool>
    {
        private readonly IRepository _repository;

        public DeletePropertyAgentHandler(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(DeletePropertyAgentRequest request, CancellationToken cancellationToken)
        {
            var propertyAgent = _repository.Get<PropertyAgent>(x => x.PropertyID == request.PropertyID && x.AgentID == request.AgentID);

            if (propertyAgent == null || propertyAgent.IsDeleted)
            {
                throw new NotFoundException("Agent not found.");
            }
            _repository.Delete<PropertyAgent>(propertyAgent);
            var result = await _repository.SaveChangesAsync();
            return result > 0;
        }
    }
}
