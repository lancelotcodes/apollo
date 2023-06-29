using apollo.Application.Common.Exceptions;
using apollo.Domain.Entities.References;
using AutoMapper;
using MediatR;
using Shared.Constants;
using Shared.Contracts;
using Shared.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace apollo.Application.Units.Commands.CreateUnit
{
    public class UnitBulkSaveHandler : IRequestHandler<UnitBulkSaveRequest, bool>
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public UnitBulkSaveHandler(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<bool> Handle(UnitBulkSaveRequest request, CancellationToken cancellationToken)
        {
            if (request != null && request.Units.IsAny())
            {
                var updatedUnits = new List<Domain.Entities.Core.Unit>();
                request.Units.ForEach(item =>
                {
                    if (item.ID > 0)
                    {
                        var unit = _repository.First<Domain.Entities.Core.Unit>(x => x.ID == item.ID, i => i.Include(x => x.UnitStatus).Include(x => x.Property.Contracts));
                        if (unit == null) throw new BadRequestException("Unit not found.");
                        var unitStatus = _repository.First<UnitStatus>(x => x.ID == item.UnitStatusID);
                        if (unit.UnitStatus.Name == AppUnitStatuses.Tenanted && unitStatus.Name == AppUnitStatuses.Vacant)
                        {
                            unit.Property.Contracts.ToList().ForEach(x => x.IsHistorical = true);
                        }
                        _mapper.Map(item, unit);
                        updatedUnits.Add(unit);
                    }
                });

                if (updatedUnits.Count > 0)
                {
                    _repository.Save(updatedUnits);
                }
            }

            return await _repository.SaveChangesAsync() > 0;
        }
    }
}