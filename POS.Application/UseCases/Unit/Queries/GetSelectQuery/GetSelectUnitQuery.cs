using MediatR;
using POS.Application.Commons.Bases;
using POS.Application.Commons.Select.Response;

namespace POS.Application.UseCases.Unit.Queries.GetSelectQuery;

public class GetSelectUnitQuery : IRequest<BaseResponse<IEnumerable<SelectResponse>>>
{
}