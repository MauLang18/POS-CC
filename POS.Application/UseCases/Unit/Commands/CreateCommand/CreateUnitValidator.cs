using FluentValidation;

namespace POS.Application.UseCases.Unit.Commands.CreateCommand;

public class CreateUnitValidator : AbstractValidator<CreateUnitCommand>
{
    public CreateUnitValidator()
    {
        RuleFor(x => x.Name)
            .NotNull().WithMessage("El Nombre no puede ser nulo.")
            .NotEmpty().WithMessage("El Nombre no puede ser vacio.");
        RuleFor(x => x.Abbreviation)
            .NotNull().WithMessage("La Abreviacion no puede ser nulo.")
            .NotEmpty().WithMessage("La Abreviacion no puede ser vacio.");
    }
}