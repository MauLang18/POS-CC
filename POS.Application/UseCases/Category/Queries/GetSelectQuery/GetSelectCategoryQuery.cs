using MediatR;
using POS.Application.Commons.Bases;
using POS.Application.Commons.Select.Response;

namespace POS.Application.UseCases.Category.Queries.GetSelectQuery;

public class GetSelectCategoryQuery : IRequest<BaseResponse<IEnumerable<SelectResponse>>>
{
}