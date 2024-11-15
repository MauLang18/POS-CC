using FluentValidation;

namespace POS.Application.UseCases.License.Commands.CreateCommand;

public class CreateLicenseValidator : AbstractValidator<CreateLicenseCommand>
{
    public CreateLicenseValidator()
    {
    }
}