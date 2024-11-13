using MediatR;
using POS.Application.Commons.Bases;
using POS.Application.Commons.Select.Response;

namespace POS.Application.UseCases.Status.Queries.GetSelectQuery;

public class GetSelectStatusQuery : IRequest<BaseResponse<IEnumerable<SelectResponse>>>
{
}