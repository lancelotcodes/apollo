using apollo.Application.Contracts.Commands;
using FluentValidation;

namespace apollo.Application.Properties.Commands.SaveProperty
{
    public class SaveContractValidator : AbstractValidator<SaveContractRequest>
    {
        public SaveContractValidator()
        {
            RuleFor(e => e.Name).NotNull().NotEmpty();
            RuleFor(e => e.ContactID).NotNull().NotEmpty();
            RuleFor(e => e.BrokerCompanyID).NotNull().NotEmpty();
            RuleFor(e => e.BrokerID).NotNull().NotEmpty();
            RuleFor(e => e.CompanyID).NotNull().NotEmpty();
            RuleFor(e => e.PropertyID).NotNull().NotEmpty();
            RuleFor(e => e.EstimatedArea).NotNull().NotEmpty();
            RuleFor(e => e.ClosingRate).NotNull().NotEmpty();
        }
    }
}
