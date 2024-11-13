using MediatR;
using POS.Application.Commons.Bases;
using POS.Application.Commons.Select.Response;

namespace POS.Application.UseCases.TemplateType.Queries.GetSelectQuery;

public class GetSelectTemplateTypeQuery : IRequest<BaseResponse<IEnumerable<SelectResponse>>>
{
}