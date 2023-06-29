using apollo.Application.OfferGeneration.Queries.DTOs;
using apollo.Domain.Entities.Core;
using apollo.Domain.Enums;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Constants;
using Shared.Contracts;
using Shared.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace apollo.Application.Properties.Queries.GetProperties
{
    public class GetOfferOptionsListQuery : IRequest<List<OfferOptionListDTO>>
    {
        public int? PropertyTypeID { get; set; }
        public int? ListingTypeID { get; set; }
        public PEZAStatus? PEZA { get; set; }
        public bool OperatingHours { get; set; }
        public int HandOverConditionID { get; set; }
        public decimal? MinSize { get; set; }
        public decimal? MaxSize { get; set; }
        public List<int> CitiesIds { get; set; }
        public List<int> ProvinceIds { get; set; }
        public List<int> SubMarketsIds { get; set; }
    }

    public class GetOfferOptionsListHandler : IRequestHandler<GetOfferOptionsListQuery, List<OfferOptionListDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IRepository _repository;

        public GetOfferOptionsListHandler(IMapper mapper, IRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<List<OfferOptionListDTO>> Handle(GetOfferOptionsListQuery request, CancellationToken cancellationToken)
        {
            var query = _repository.Fetch<Property>(x => !x.IsDeleted,
                        i =>
                        i.Include(x => x.Grade)
                        .Include(x => x.PropertyType)
                        .Include(x => x.Contact)
                        .Include(x => x.Building).Include(x => x.Address)
                        .Include(x => x.Building).ThenInclude(x => x.Floors)
                        .ThenInclude(x => x.Units.Where(x => x.Availability.Name == AppUnitAvailability.Available && (!request.MaxSize.HasValue || x.LeaseFloorArea <= request.MaxSize)))
                        .ThenInclude(x => x.ListingType)
                        .Include(x => x.Building).ThenInclude(x => x.Floors)
                        .ThenInclude(x => x.Units.Where(x => x.Availability.Name == AppUnitAvailability.Available && (!request.MaxSize.HasValue || x.LeaseFloorArea <= request.MaxSize)))
                        .ThenInclude(x => x.Availability)
                        .Include(x => x.Building).ThenInclude(x => x.Floors)
                        .ThenInclude(x => x.Units.Where(x => x.Availability.Name == AppUnitAvailability.Available && (!request.MaxSize.HasValue || x.LeaseFloorArea <= request.MaxSize)))
                        .ThenInclude(x => x.HandOverCondition)
                         .Include(x => x.Building).ThenInclude(x => x.Floors)
                        .ThenInclude(x => x.Units.Where(x => x.Availability.Name == AppUnitAvailability.Available && (!request.MaxSize.HasValue || x.LeaseFloorArea <= request.MaxSize)))
                        .ThenInclude(x => x.UnitStatus)
                        .Include(x => x.Address).ThenInclude(x => x.City)
                        .Include(x => x.Address).ThenInclude(x => x.SubMarket)
                        .Include(x => x.Address).ThenInclude(x => x.MicroDistrict)
                        .Include(x => x.Agents).ThenInclude(x => x.Agent)
                        .Include(i => i.PropertyDocuments.Where(x => x.IsPrimary == true
                                        && x.IsDeleted == false
                                        && x.DocumentType == PropertyDocumentType.MainImage))
                        .ThenInclude(x => x.Document));

            if (request.PropertyTypeID.HasValue)
            {
                query.Where(x => x.PropertyTypeID == request.PropertyTypeID.Value);
            }

            if (request.ProvinceIds != null && request.ProvinceIds.Any())
            {
                query = query.Where(x => request.ProvinceIds.Contains(x.Address.City.ProvinceID));
            }

            if (request.CitiesIds != null && request.CitiesIds.Any())
            {
                query = query.Where(x => request.CitiesIds.Contains(x.Address.CityID));
            }

            if (request.SubMarketsIds != null && request.SubMarketsIds.Any())
            {
                query = query.Where(x => x.Address.SubMarketID.HasValue && request.SubMarketsIds.Contains(x.Address.SubMarketID.Value));
            }

            if (request.PEZA.HasValue)
            {
                query = query.Where(x => x.Building.PEZA == request.PEZA.Value);
            }

            if (request.OperatingHours)
            {
                query = query.Where(x => x.Building.OperatingHours == true);
            }

            if ((request.MinSize.HasValue && request.MinSize > 0) || (request.MaxSize.HasValue && request.MaxSize > 0))
            {
                query = query.Where(x => (!request.MinSize.HasValue || x.Building.Floors.SelectMany(x => x.Units)
                .Where(x => x.Availability.Name == AppUnitAvailability.Available)
                .Sum(x => x.LeaseFloorArea) >= request.MinSize
                || x.Building.Floors.SelectMany(x => x.Units).Any(x => x.LeaseFloorArea >= request.MinSize))
                && (!request.MaxSize.HasValue || x.Building.Floors.SelectMany(x => x.Units)
                .Where(x => x.Availability.Name == AppUnitAvailability.Available)
                .Sum(x => x.LeaseFloorArea) <= request.MaxSize
                || x.Building.Floors.SelectMany(x => x.Units).Any(x => x.LeaseFloorArea <= request.MaxSize)));
            }

            query = query.Where(x => x.Building.Floors.SelectMany(x => x.Units).Count() > 0);

            return _mapper.Map<List<OfferOptionListDTO>>(await query.ToListAsync());
        }
    }
}
