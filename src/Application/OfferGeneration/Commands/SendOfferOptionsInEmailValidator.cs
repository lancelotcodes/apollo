using FluentValidation;

namespace apollo.Application.Agents.Commands.SavePropertyAgent
{
    public class SendOfferOptionsInEmailValidator : AbstractValidator<SendOfferOptionsInEmailRequest>
    {
        public SendOfferOptionsInEmailValidator()
        {
            RuleFor(e => e.AgentID).NotEmpty();
            RuleFor(e => e.ContactID).NotEmpty();
        }
    }
}
