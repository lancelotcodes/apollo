using apollo.Application.Common.Exceptions;
using apollo.Application.Common.Interfaces;
using apollo.Domain.Entities.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace apollo.Application.Companies.Commands.CreateCompany
{
    public class CreateCompanyCommand : IRequest<int>
    {
        public string Name { get; set; }
    }

    public class CreateCompanyHandler : IRequestHandler<CreateCompanyCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreateCompanyHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
        {
            var existingCompany = await _context.Companies.FirstOrDefaultAsync(x => x.Name == request.Name);

            if (existingCompany != null)
            {
                throw new GenericException("Company already exist with the same name.");
            }

            Company company = new Company
            {
                Name = request.Name,
                IsDeleted = false
            };
            _context.Companies.Add(company);

            await _context.SaveChangesAsync(cancellationToken);
            return company.ID;
        }
    }
}