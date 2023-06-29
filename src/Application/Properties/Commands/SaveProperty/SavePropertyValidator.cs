using FluentValidation;

namespace apollo.Application.Properties.Commands.SaveProperty
{
    public class SavePropertyValidator : AbstractValidator<SavePropertyRequest>
    {
        public SavePropertyValidator()
        {
            RuleFor(e => e.Name).NotNull().NotEmpty();
            RuleFor(e => e.PropertyTypeID).NotEmpty();
            RuleFor(e => e.GradeID).NotEmpty();
        }
    }
}
