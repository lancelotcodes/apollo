using apollo.Application.Common.Exceptions;
using apollo.Application.Common.Models;
using apollo.Domain.Entities.Core;
using apollo.Domain.Entities.Shared;
using apollo.Domain.Enums;
using AutoMapper;
using MediatR;
using Shared.Contracts;
using System.Threading;
using System.Threading.Tasks;

namespace apollo.Application.PropertySEO.Commands.SavePropertySEO
{
    public class SavePropertySEOHandler : IRequestHandler<SavePropertySEORequest, SaveEntityResult>
    {
        private readonly IMapper _mapper;
        private readonly IRepository _repository;

        public SavePropertySEOHandler(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<SaveEntityResult> Handle(SavePropertySEORequest request, CancellationToken cancellationToken)
        {
            var seo = _mapper.Map<SEO>(request);
            seo.PublishType = PublishType.Public;
            if (seo == null) throw new BadRequestException("Unable to create SEO.");

            _repository.Save(seo);
            await _repository.SaveChangesAsync();

            if (request.ID == 0)
            {
                var property = _repository.GetById<Property>(request.PropertyID);
                property.SEOID = seo.ID;
                await _repository.SaveChangesAsync();
            }

            return new SaveEntityResult { EntityId = seo.ID };
        }
    }
}
