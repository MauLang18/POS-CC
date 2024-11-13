using FluentValidation;

namespace POS.Application.UseCases.TemplateType.Commands.CreateCommand;

public class CreateTemplateTypeValidator : AbstractValidator<CreateTemplateTypeCommand>
{
    public CreateTemplateTypeValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("El Nombre no puede ser vacío")
            .NotNull().WithMessage("El Nombre no puede ser nulo.");
    }
}