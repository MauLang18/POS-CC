using FluentValidation;

namespace POS.Application.UseCases.Supplier.Commands.CreateCommand;

public class CreateSupplierValidator : AbstractValidator<CreateSupplierCommand>
{
    public CreateSupplierValidator()
    {
        RuleFor(x => x.Name)
            .NotNull().WithMessage("El Nombre no puede ser nulo.")
            .NotEmpty().WithMessage("El Nombre no puede ser vacio.");
        RuleFor(x => x.DocumentNumber)
            .NotNull().WithMessage("El Numero de Documento no puede ser nulo.")
            .NotEmpty().WithMessage("El Numero de Documento no puede ser vacio.");
    }
}