using apollo.Application.Common.Exceptions;
using apollo.Application.Common.Interfaces;
using apollo.Domain.Entities.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace apollo.Application.Contacts.Commands.CreateContact
{
    public class CreateContactCommand : IRequest<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public int CompanyID { get; set; }
    }

    public class CreateContactHandler : IRequestHandler<CreateContactCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreateContactHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateContactCommand request, CancellationToken cancellationToken)
        {
            var existingContact = await _context.Contacts.FirstOrDefaultAsync(x => x.FirstName == request.FirstName && x.LastName == request.LastName && x.PhoneNumber == request.PhoneNumber && x.Email == request.Email);

            if (existingContact != null)
            {
                throw new GenericException("Contact with same Email and Phone Number already exist.");
            }

            Contact contact = new Contact
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                PhoneNumber = request.PhoneNumber,
                Email = request.Email,
                IsDeleted = false,
                CompanyID = request.CompanyID,
            };

            _context.Contacts.Add(contact);
            await _context.SaveChangesAsync(cancellationToken);

            return contact.ID;
        }
    }
}