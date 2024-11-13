using MediatR;
using POS.Application.Commons.Bases;
using POS.Application.Dtos.Unit.Response;

namespace POS.Application.UseCases.Unit.Queries.GetAllQuery;

public class GetAllUnitQuery : BaseFilters, IRequest<BaseResponse<IEnumerable<UnitResponseDto>>>
{
}