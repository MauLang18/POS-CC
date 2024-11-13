using FluentValidation;

namespace POS.Application.UseCases.DocumentTemplate.Commands.CreateCommand;

public class CreateDocumentTemplateValidator : AbstractValidator<CreateDocumentTemplateCommand>
{
    public CreateDocumentTemplateValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("El Nombre no puede ser vacío")
            .NotNull().WithMessage("El Nombre no puede ser nulo.");
        RuleFor(x => x.Content)
            .NotEmpty().WithMessage("El Contenido no puede ser vacío")
            .NotNull().WithMessage("El Contenido no puede ser nulo.");
    }
}