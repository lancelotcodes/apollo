using apollo.Application.Grades.DTOs;
using apollo.Domain.Entities.References;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Contracts;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;


namespace apollo.Application.Grades.GetGrades
{
    public class GetShortGradeQuery : IRequest<IEnumerable<ShortGradeListDTO>>
    {
        public int PropertyTypeID { get; set; }
    }

    public class GetShortGradeQueryHandler : IRequestHandler<GetShortGradeQuery, IEnumerable<ShortGradeListDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IRepository _repository;

        public GetShortGradeQueryHandler(IMapper mapper, IRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<IEnumerable<ShortGradeListDTO>> Handle(GetShortGradeQuery request, CancellationToken cancellationToken)
        {
            var properties = _repository.Fetch<Grade>(g => g.PropertyTypeID == request.PropertyTypeID && !g.IsDeleted);

            return await properties.ProjectTo<ShortGradeListDTO>(_mapper.ConfigurationProvider).ToListAsync();
        }
    }
}
