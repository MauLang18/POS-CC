using FluentValidation;

namespace POS.Application.UseCases.Category.Commands.CreateCommand;

public class CreateCategoryValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("El Nombre no puede ser vacío")
            .NotNull().WithMessage("El Nombre no puede ser nulo.");
    }
}