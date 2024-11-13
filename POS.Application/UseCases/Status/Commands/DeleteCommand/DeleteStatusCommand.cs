using MediatR;
using POS.Application.Commons.Bases;

namespace POS.Application.UseCases.Status.Commands.DeleteCommand;

public class DeleteStatusCommand : IRequest<BaseResponse<bool>>
{
    public int StatusId { get; set; }
}