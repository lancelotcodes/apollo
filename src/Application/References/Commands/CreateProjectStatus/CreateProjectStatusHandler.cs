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

namespace apollo.Application.References.Commands.CreateProjectStatus
{
    public class CreateProjectStatusHandler : IRequestHandler<CreateProjectStatusCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreateProjectStatusHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateProjectStatusCommand request, CancellationToken cancellationToken)
        {
            ProjectStatus projectStatus = new ProjectStatus
            {
                Name = request.Name,
                IsDeleted = false
            };
            _context.ProjectStatuses.Add(projectStatus);
            try
            {
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (Exception err)
            {
                throw new GenericException(err.Message);
            }

            return projectStatus.ID;
        }
    }
}
