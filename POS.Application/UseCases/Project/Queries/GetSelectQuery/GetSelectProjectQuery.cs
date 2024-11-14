using MediatR;
using POS.Application.Commons.Bases;
using POS.Application.Commons.Select.Response;

namespace POS.Application.UseCases.Project.Queries.GetSelectQuery;

public class GetSelectProjectQuery : IRequest<BaseResponse<IEnumerable<SelectResponse>>>
{
}