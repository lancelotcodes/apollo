using FluentValidation;

namespace apollo.Application.Documents.Commands.SavePropertyVideo
{
    public class SavePropertyVideoValidator : AbstractValidator<SavePropertyVideoRequest>
    {
        public SavePropertyVideoValidator()
        {
            RuleFor(e => e.PropertyID).NotEmpty();
        }
    }
}
