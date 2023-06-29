using FluentValidation;

namespace apollo.Application.Documents.Commands.SavePropertyDocuments
{
    public class SavePropertyDocumentValidator : AbstractValidator<SavePropertyDocumentRequest>
    {
        public SavePropertyDocumentValidator()
        {
            RuleFor(e => e.PropertyID).NotEmpty();
        }
    }
}
