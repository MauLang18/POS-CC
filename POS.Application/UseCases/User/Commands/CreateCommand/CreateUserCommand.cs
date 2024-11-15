using MediatR;
using POS.Application.Commons.Bases;

namespace POS.Application.UseCases.User.Commands.CreateCommand;

public class CreateUserCommand : IRequest<BaseResponse<bool>>
{
    public string UserName { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Email { get; set; } = null!;
    public int State { get; set; }
}