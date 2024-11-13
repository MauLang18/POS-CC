using MediatR;
using POS.Application.Commons.Bases;

namespace POS.Application.UseCases.Status.Commands.CreateCommand;

public class CreateStatusCommand : IRequest<BaseResponse<bool>>
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public int State { get; set; }
}