using MediatR;
using POS.Application.Commons.Bases;

namespace POS.Application.UseCases.Status.Commands.UpdateCommand;

public class UpdateStatusCommand : IRequest<BaseResponse<bool>>
{
    public int StatusId { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public int State { get; set; }
}