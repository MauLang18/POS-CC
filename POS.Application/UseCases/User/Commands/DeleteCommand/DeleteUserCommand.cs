using MediatR;
using POS.Application.Commons.Bases;

namespace POS.Application.UseCases.User.Commands.DeleteCommand;

public class DeleteUserCommand : IRequest<BaseResponse<bool>>
{
    public int UserId { get; set; }
}