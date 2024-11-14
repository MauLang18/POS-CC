using FluentValidation;

namespace POS.Application.UseCases.EmailTemplate.Commands.CreateCommand;

public class CreateEmailTemplateValidator : AbstractValidator<CreateEmailTemplateCommand>
{
    public CreateEmailTemplateValidator()
    {
        RuleFor(x => x.Subject)
            .NotEmpty().WithMessage("El Asunto no puede ser vacío")
            .NotNull().WithMessage("El Asunto no puede ser nulo.");
        RuleFor(x => x.Body)
            .NotEmpty().WithMessage("El Contenido no puede ser vacío")
            .NotNull().WithMessage("El Contenido no puede ser nulo.");
    }
}