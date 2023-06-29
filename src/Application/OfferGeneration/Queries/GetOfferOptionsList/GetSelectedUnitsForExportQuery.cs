using apollo.Application.Agents.Queries.DTOs;
using apollo.Application.OfferGeneration.Queries.DTOs;
using apollo.Domain.Entities.Core;
using apollo.Domain.Enums;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Contracts;
using Shared.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace apollo.Application.Properties.Queries.GetProperties
{
    public class GetSelectedUnitsForExportQuery : IRequest<SelectedOfferUnitsDTO>
    {
        public int ContactID { get; set; }
        public int AgentID { get; set; }
        public List<int> UnitIds { get; set; }
    }

    public class GetSelectedUnitsForExportHandler : IRequestHandler<GetSelectedUnitsForExportQuery, SelectedOfferUnitsDTO>
    {
        private readonly IMapper _mapper;
        private readonly IRepository _repository;

        public GetSelectedUnitsForExportHandler(IMapper mapper, IRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<SelectedOfferUnitsDTO> Handle(GetSelectedUnitsForExportQuery request, CancellationToken cancellationToken)
        {
            if (request.ContactID == 0 || request.AgentID == 0)
            {
                return null;
            }

            var response = new SelectedOfferUnitsDTO();

            var query = _repository.Fetch<Domain.Entities.Core.Unit>(x => !x.IsDeleted && request.UnitIds.Contains(x.ID), i =>
            i.Include(x => x.Floor.Building.Property.Address)
            .ThenInclude(x => x.City)
            .Include(x => x.Floor.Building.Property.Address)
            .ThenInclude(x => x.SubMarket)
            .Include(x => x.Availability)
            .Include(x => x.HandOverCondition)
            .Include(x => x.UnitStatus)
            .Include(x => x.Floor.Building.Developer)
            .Include(x => x.Floor.Building.Property.Grade)
            .Include(x => x.Floor.Building)
            .ThenInclude(i => i.Property.PropertyDocuments.Where(x => x.IsPrimary == true
                                        && x.IsDeleted == false
                                        && (x.DocumentType == PropertyDocumentType.MainImage || x.DocumentType == PropertyDocumentType.FloorPlanImage))).ThenInclude(x => x.Document));
            response.Units = _mapper.Map<List<UnitDetailForOfferExportDTO>>(await query.ToListAsync());
            response.Agent = _mapper.Map<ContactDTO>(await _repository.UntrackFirstAsync<Contact>(x => x.ID == request.AgentID));

            var contact = await _repository.UntrackFirstAsync<Contact>(x => x.ID == request.ContactID);
            response.ContactName = contact.FirstName + " " + contact.LastName;

            return response;
        }
    }
}
