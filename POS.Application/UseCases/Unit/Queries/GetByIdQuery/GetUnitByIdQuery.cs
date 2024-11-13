using MediatR;
using POS.Application.Commons.Bases;
using POS.Application.Dtos.Unit.Response;

namespace POS.Application.UseCases.Unit.Queries.GetByIdQuery;

public class GetUnitByIdQuery : IRequest<BaseResponse<UnitByIdResponseDto>>
{
    public int UnitId { get; set; }
}