using apollo.Application.Common.Interfaces;
using apollo.Domain.Entities.Core;
using apollo.Domain.Entities.References;
using AutoMapper;
using MediatR;
using Shared.Contracts;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace apollo.Application.Agents.Commands.SavePropertyAgent
{
    public class SendOfferOptionsInEmailHandler : IRequestHandler<SendOfferOptionsInEmailRequest, bool>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public SendOfferOptionsInEmailHandler(ICurrentUserService currentUserService, IRepository repository, IMapper mapper)
        {
            _currentUserService = currentUserService;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<bool> Handle(SendOfferOptionsInEmailRequest request, CancellationToken cancellationToken)
        {
            if (request.UnitIds == null || !request.UnitIds.Any())
            {
                return false;
            }

            var offer = new OfferOption();
            _mapper.Map(request, offer);
            offer.UserID = _currentUserService.UserId;
            offer.OfferUnits = new Collection<OfferUnit>();

            if (request.CityIds != null && request.CityIds.Any())
            {
                var cities = _repository.Fetch<City>(x => request.CityIds.Contains(x.ID)).ToList();
                if (cities != null && cities.Any())
                {
                    offer.Cities = string.Join(",", cities.Select(x => x.Name).ToList());
                }
            }

            if (request.ProvinceIds != null && request.ProvinceIds.Any())
            {
                var provinces = _repository.Fetch<Province>(x => request.ProvinceIds.Contains(x.ID)).ToList();
                if (provinces != null && provinces.Any())
                {
                    offer.Provinces = string.Join(",", provinces.Select(x => x.Name).ToList());
                }
            }

            if (request.SubMarketIds != null && request.SubMarketIds.Any())
            {
                var subMarkets = _repository.Fetch<Province>(x => request.SubMarketIds.Contains(x.ID)).ToList();
                if (subMarkets != null && subMarkets.Any())
                {
                    offer.SubMarkets = string.Join(",", subMarkets.Select(x => x.Name).ToList());
                }
            }

            request.UnitIds.ForEach(x => offer.OfferUnits.Add(new OfferUnit { UnitId = x }));
            _repository.Save(offer);
            var result = await _repository.SaveChangesAsync();

            return result > 0;
        }
    }
}
