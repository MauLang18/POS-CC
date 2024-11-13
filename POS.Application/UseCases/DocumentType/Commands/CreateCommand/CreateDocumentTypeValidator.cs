using FluentValidation;

namespace POS.Application.UseCases.DocumentType.Commands.CreateCommand;

public class CreateDocumentTypeValidator : AbstractValidator<CreateDocumentTypeCommand>
{
    public CreateDocumentTypeValidator()
    {
        RuleFor(x => x.Name)
            .NotNull().WithMessage("El Nombre no puede ser nulo.")
            .NotEmpty().WithMessage("El Nombre no puede ser vacio.");
        RuleFor(x => x.Abbreviation)
            .NotNull().WithMessage("La Abreviacion no puede ser nulo.")
            .NotEmpty().WithMessage("La Abreviacion no puede ser vacio.");
    }
}