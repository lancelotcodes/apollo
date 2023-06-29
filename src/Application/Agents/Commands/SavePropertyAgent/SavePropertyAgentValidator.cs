using FluentValidation;

namespace apollo.Application.Agents.Commands.SavePropertyAgent
{
    public class SavePropertyAgentValidator : AbstractValidator<SavePropertyAgentRequest>
    {
        public SavePropertyAgentValidator()
        {
            RuleFor(e => e.PropertyID).NotEmpty();
        }
    }
}
