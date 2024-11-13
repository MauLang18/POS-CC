using FluentValidation;

namespace POS.Application.UseCases.VoucherType.Commands.CreateCommand;

public class CreateVoucherTypeValidator : AbstractValidator<CreateVoucherTypeCommand>
{
    public CreateVoucherTypeValidator()
    {
        RuleFor(x => x.Name)
            .NotNull().WithMessage("El Nombre no puede ser nulo.")
            .NotEmpty().WithMessage("El Nombre no puede ser vacio.");
        RuleFor(x => x.Abbreviation)
            .NotNull().WithMessage("La Abreviacion no puede ser nulo.")
            .NotEmpty().WithMessage("La Abreviacion no puede ser vacio.");
    }
}