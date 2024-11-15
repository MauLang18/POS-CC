using FluentValidation;

namespace POS.Application.UseCases.LicenseType.Commands.CreateCommand;

public class CreateLicenseTypeValidator : AbstractValidator<CreateLicenseTypeCommand>
{
    public CreateLicenseTypeValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("El Nombre no puede ser vacío")
            .NotNull().WithMessage("El Nombre no puede ser nulo.");
    }
}