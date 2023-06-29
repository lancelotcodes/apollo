using FluentValidation;

namespace apollo.Application.Properties.Commands.ConfirmProperty
{
    public class ConfirmPropertyValidator : AbstractValidator<ConfirmPropertyRequest>
    {
        public ConfirmPropertyValidator()
        {
            RuleFor(e => e.PropertyID).NotEmpty();
        }
    }
}
