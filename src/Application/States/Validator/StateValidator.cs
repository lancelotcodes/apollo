using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using apollo.Application.States.Commands.CreateState;
using apollo.Application.States.Commands.UpdateState;

namespace apollo.Application.States.Validator
{
    public class StateValidator : AbstractValidator<CreateStateCommand>
    {
        public StateValidator()
        {
            RuleFor(i => i.Name).NotEmpty().WithMessage("State name must not be empty");
        }
    }
    public class CreateStateCityValidator : AbstractValidator<AddStateCityDTO>
    {
        public CreateStateCityValidator()
        {
            RuleFor(i => i.Name).NotEmpty().WithMessage("State name must not be empty");
        }
    }

    public class UpdateStateValidator : AbstractValidator<UpdateStateCommand>
    {
        public UpdateStateValidator()
        {
            RuleFor(i => i.Name).NotEmpty().WithMessage("State name must not be empty");
        }
    }
}
