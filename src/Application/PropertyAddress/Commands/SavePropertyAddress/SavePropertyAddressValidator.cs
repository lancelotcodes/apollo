using FluentValidation;

namespace apollo.Application.PropertyAddress.Commands.SavePropertyAddress
{
    public class SavePropertyAddressValidator : AbstractValidator<SavePropertyAddressRequest>
    {
        public SavePropertyAddressValidator()
        {
            RuleFor(e => e.PropertyID).NotEmpty();
            RuleFor(e => e.CityID).NotEmpty();
            RuleFor(e => e.Line1).NotEmpty();
            RuleFor(e => e.Latitude).NotEmpty();
            RuleFor(e => e.Longitude).NotEmpty();
        }
    }
}
