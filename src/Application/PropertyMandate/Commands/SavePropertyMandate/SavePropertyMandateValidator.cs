using FluentValidation;

namespace apollo.Application.PropertyMandate.Commands.SavePropertyMandate
{
    public class SavePropertyMandateValidator : AbstractValidator<PropertyMandateRequest>
    {
        public SavePropertyMandateValidator()
        {
            RuleFor(e => e.PropertyID).NotEmpty();
            RuleFor(e => e.Name).NotEmpty();
            RuleFor(e => e.AttachmentId).NotEmpty();
        }
    }
}
