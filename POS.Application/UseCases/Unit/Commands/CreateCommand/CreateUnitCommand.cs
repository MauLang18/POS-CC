using MediatR;
using POS.Application.Commons.Bases;

namespace POS.Application.UseCases.Unit.Commands.CreateCommand;

public class CreateUnitCommand : IRequest<BaseResponse<bool>>
{
    public string Name { get; set; } = null!;
    public string Abbreviation { get; set; } = null!;
    public int State { get; set; }
}