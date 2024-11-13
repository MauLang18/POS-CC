using FluentValidation;

namespace POS.Application.UseCases.CreditType.Commands.CreateCommand;

public class CreateCreditTypeValidator : AbstractValidator<CreateCreditTypeCommand>
{
    public CreateCreditTypeValidator()
    {
        RuleFor(x => x.Name)
            .NotNull().WithMessage("El Nombre no puede ser nulo.")
            .NotEmpty().WithMessage("El Nombre no puede ser vacio.");
    }
}