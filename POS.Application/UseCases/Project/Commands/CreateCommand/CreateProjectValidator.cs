using FluentValidation;

namespace POS.Application.UseCases.Project.Commands.CreateCommand;

public class CreateProjectValidator : AbstractValidator<CreateProjectCommand>
{
    public CreateProjectValidator()
    {
        RuleFor(x => x.InternalName)
            .NotEmpty().WithMessage("El Nombre Interno no puede ser vacío")
            .NotNull().WithMessage("El Nombre Interno no puede ser nulo.");
        RuleFor(x => x.CommercialName)
            .NotEmpty().WithMessage("El Nombre Comercial no puede ser vacío")
            .NotNull().WithMessage("El Nombre Comercial no puede ser nulo.");
    }
}