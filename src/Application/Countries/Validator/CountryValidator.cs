using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using apollo.Application.Countries.Commands.CreateCountry;
using apollo.Application.Countries.Commands.UpdateCountry;

namespace apollo.Application.Countries.Validator
{
    public class CountryValidator : AbstractValidator<AddCountryCommand>
    {
        public CountryValidator()
        {
            RuleFor(i => i.Name).NotEmpty().WithMessage("Country name must not be empty");
        }
    }

    public class AddCountryStateDTOValidator : AbstractValidator<AddCountryStateDTO>
    {
        public AddCountryStateDTOValidator()
        {
            RuleFor(i => i.Name).NotEmpty().WithMessage("State name must not be empty");
        }
    }

    public class AddCountryCityDTOValidator : AbstractValidator<AddCountryCityDTO>
    {
        public AddCountryCityDTOValidator()
        {
            RuleFor(i => i.Name).NotEmpty().WithMessage("City name must not be empty");
        }
    }
    public class UpdateCountryValidator : AbstractValidator<UpdateCountryCommand>
    {
        public UpdateCountryValidator()
        {
            RuleFor(i => i.Name).NotEmpty().WithMessage("Country name must not be empty");
        }
    }
}
