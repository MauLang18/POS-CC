using MediatR;
using POS.Application.Commons.Bases;
using POS.Application.Commons.Select.Response;

namespace POS.Application.UseCases.DocumentTemplate.Queries.GetSelectQuery;

public class GetSelectDocumentTemplateQuery : IRequest<BaseResponse<IEnumerable<SelectResponse>>>
{
}