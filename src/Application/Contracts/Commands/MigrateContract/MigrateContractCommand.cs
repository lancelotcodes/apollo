using apollo.Application.Common.Exceptions;
using apollo.Application.Common.Interfaces;
using apollo.Application.Common.Models;
using apollo.Application.Companies.Commands.CreateCompany;
using apollo.Application.Contacts.Commands.CreateContact;
using apollo.Application.Contracts.Commands;
using apollo.Domain.Entities.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace apollo.Application.Buildings.Commands.MigrateContract
{
    public class MigrateContractCommand : IRequest<MigrationResultModel>
    {
        public List<MigrateContractDTO> Data { get; set; }

        public class Handler : IRequestHandler<MigrateContractCommand, MigrationResultModel>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMediator _mediator;

            public Handler(IApplicationDbContext context, IMediator mediator)
            {
                _context = context;
                _mediator = mediator;
            }
            public async Task<MigrationResultModel> Handle(MigrateContractCommand request, CancellationToken cancellationToken)
            {
                List<string> errors = new List<string>();

                var totalCount = request.Data.Count();
                var totalUploaded = 0;

                foreach (var data in request.Data)
                {
                    var refJSONString = JsonConvert.SerializeObject(data);
                    var csvEntity = JsonConvert.DeserializeObject<MigrateContractDTO>(refJSONString);

                    int companyID = 0;
                    int contactID = 0;

                    if (String.IsNullOrEmpty(csvEntity.Name))
                    {
                        errors.Add($"Missing property name at row {request.Data.IndexOf(data) + 1}");
                    }

                    Company existingCompany = null;
                    Contact existingContact = null;

                    try
                    {
                        existingCompany = await _context.Companies.FirstOrDefaultAsync(i => i.Name.ToLower() == csvEntity.Tenant.ToLower());
                    } catch { }

                    try
                    {
                        existingContact = await _context.Contacts.FirstOrDefaultAsync(i => i.FirstName.ToLower() == csvEntity.FirstName.ToLower() && i.LastName.ToLower() == csvEntity.LastName.ToLower());
                    }
                    catch { }

                    if (existingCompany != null)
                    {
                        companyID = existingCompany.ID;
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(csvEntity.Tenant))
                        {
                            var createCompanyCommand = new CreateCompanyCommand
                            {
                                Name = csvEntity.Name
                            };
                            companyID = await _mediator.Send(createCompanyCommand);
                        }
                    }

                    if (existingContact != null)
                    {
                        contactID = existingContact.ID;
                    }
                    else
                    {
                        if ((!string.IsNullOrEmpty(csvEntity.FirstName)) && (!string.IsNullOrEmpty(csvEntity.LastName)))
                        {
                            var createContactCommand = new CreateContactCommand
                            {
                                FirstName = csvEntity.FirstName,
                                LastName = csvEntity.LastName,
                                Email = csvEntity.Email,
                                PhoneNumber = csvEntity.PhoneNumber
                            };
                            contactID = await _mediator.Send(createContactCommand);
                        }
                    }

                    if (errors.Count() == 0)
                    {
                        if (!string.IsNullOrEmpty(csvEntity.Name))
                        {
                            try
                            {
                                var createCommand = new SaveContractRequest
                                {
                                    Name = csvEntity.Name,
                                    PropertyID = csvEntity.PropertyID,
                                    CompanyID = companyID,
                                    ContactID = contactID,
                                    StartDate = csvEntity.StartDate,
                                    EndDate = csvEntity.EndDate,
                                    TenantClassificationID = csvEntity.TenantClassificationID,
                                    EstimatedArea = csvEntity.EstimatedArea,
                                    LeaseTerm = csvEntity.LeaseTerm,
                                    BrokerID = csvEntity.BrokerID,
                                    BrokerCompanyID = csvEntity.BrokerCompanyID,
                                    IsHistorical = csvEntity.IsHistorical
                                };
                                var id = await _mediator.Send(createCommand);
                                totalUploaded++;
                                

                            }
                            catch (Exception err)
                            {
                                errors.Add(err.Message);
                            }
                        }
                    }
                }

                return new MigrationResultModel
                {
                    Succeeded = errors.Count > 0 ? false : true,
                    Uploaded = totalUploaded,
                    Total = totalCount,
                    Errors = errors
                };
            }
        }

    }
}