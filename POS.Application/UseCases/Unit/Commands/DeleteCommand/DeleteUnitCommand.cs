using MediatR;
using POS.Application.Commons.Bases;

namespace POS.Application.UseCases.Unit.Commands.DeleteCommand;

public class DeleteUnitCommand : IRequest<BaseResponse<bool>>
{
    public int UnitId { get; set; }
}