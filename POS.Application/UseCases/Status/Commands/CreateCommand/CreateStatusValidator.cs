using FluentValidation;

namespace POS.Application.UseCases.Status.Commands.CreateCommand;

public class CreateStatusValidator : AbstractValidator<CreateStatusCommand>
{
    public CreateStatusValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("El Nombre no puede ser vacío")
            .NotNull().WithMessage("El Nombre no puede ser nulo.");
    }
}