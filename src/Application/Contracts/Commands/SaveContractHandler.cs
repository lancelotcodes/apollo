using apollo.Application.Common.Exceptions;
using apollo.Application.Common.Models;
using apollo.Domain.Entities.Core;
using AutoMapper;
using MediatR;
using Shared.Contracts;
using System.Threading;
using System.Threading.Tasks;

namespace apollo.Application.Contracts.Commands
{
    public class SaveContractHandler : IRequestHandler<SaveContractRequest, SaveEntityResult>
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public SaveContractHandler(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<SaveEntityResult> Handle(SaveContractRequest request, CancellationToken cancellationToken)
        {
            var contract = new Contract();

            if (request.ID > 0)
            {
                contract = _repository.GetById<Contract>(request.ID);
                if (contract == null) throw new BadRequestException("Unable to save contract.");
            }

            _mapper.Map(request, contract);

            _repository.Save(contract);
            await _repository.SaveChangesAsync();

            return new SaveEntityResult { EntityId = contract.ID };
        }
    }
}
