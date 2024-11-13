using FluentValidation;

namespace POS.Application.UseCases.ProductService.Commands.CreateCommand;

public class CreateProductServiceValidator : AbstractValidator<CreateProductServiceCommand>
{
    public CreateProductServiceValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("El Nombre no puede ser vacío")
            .NotNull().WithMessage("El Nombre no puede ser nulo.");
    }
}