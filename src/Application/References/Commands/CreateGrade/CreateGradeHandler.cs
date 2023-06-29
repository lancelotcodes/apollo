using apollo.Application.Common.Exceptions;
using apollo.Application.Common.Interfaces;
using apollo.Domain.Entities.References;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace apollo.Application.References.Commands.CreateGrade
{
    public class CreateGradeHandler : IRequestHandler<CreateGradeCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreateGradeHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateGradeCommand request, CancellationToken cancellationToken)
        {
            Grade grade = new Grade
            {
                Name = request.Name,
                PropertyTypeID = request.PropertyTypeID,
                IsDeleted = false
            };
            _context.Grades.Add(grade);
            try
            {
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (Exception err)
            {
                throw new GenericException(err.Message);
            }

            return grade.ID;
        }
    }
}
