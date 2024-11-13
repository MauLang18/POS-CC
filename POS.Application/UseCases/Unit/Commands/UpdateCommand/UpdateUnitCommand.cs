using MediatR;
using POS.Application.Commons.Bases;

namespace POS.Application.UseCases.Unit.Commands.UpdateCommand;

public class UpdateUnitCommand : IRequest<BaseResponse<bool>>
{
    public int UnitId { get; set; }
    public string Name { get; set; } = null!;
    public string? Abbreviation { get; set; }
    public int State { get; set; }
}