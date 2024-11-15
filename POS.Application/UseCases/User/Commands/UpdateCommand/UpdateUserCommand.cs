using MediatR;
using POS.Application.Commons.Bases;

namespace POS.Application.UseCases.User.Commands.UpdateCommand;

public class UpdateUserCommand : IRequest<BaseResponse<bool>>
{
    public int UserId { get; set; }
    public string UserName { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Email { get; set; } = null!;
    public int State { get; set; }
}