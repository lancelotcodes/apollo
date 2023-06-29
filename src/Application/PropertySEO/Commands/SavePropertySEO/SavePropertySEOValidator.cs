using FluentValidation;

namespace apollo.Application.PropertySEO.Commands.SavePropertySEO
{
    public class SavePropertySEOValidator : AbstractValidator<SavePropertySEORequest>
    {
        public SavePropertySEOValidator()
        {
            RuleFor(e => e.PropertyID).NotEmpty();
            RuleFor(e => e.PageDescription).NotEmpty();
            RuleFor(e => e.PageTitle).NotEmpty();
            RuleFor(e => e.MetaKeyword).NotEmpty();
            RuleFor(e => e.Url).NotEmpty();
        }
    }
}
