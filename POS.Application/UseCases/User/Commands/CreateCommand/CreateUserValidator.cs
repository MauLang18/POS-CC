using FluentValidation;

namespace POS.Application.UseCases.User.Commands.CreateCommand;

public class CreateUserValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserValidator()
    {
        RuleFor(x => x.UserName)
            .NotNull().WithMessage("El Usuario no puede ser nulo.")
            .NotEmpty().WithMessage("El Usuario no puede ser vacio.");
        RuleFor(x => x.Password)
            .NotNull().WithMessage("La Contraseña no puede ser nulo.")
            .NotEmpty().WithMessage("La Contraseña no puede ser vacio.");
        RuleFor(x => x.Email)
            .NotNull().WithMessage("El Correo no puede ser nulo.")
            .NotEmpty().WithMessage("El Correo no puede ser vacio.");
    }
}