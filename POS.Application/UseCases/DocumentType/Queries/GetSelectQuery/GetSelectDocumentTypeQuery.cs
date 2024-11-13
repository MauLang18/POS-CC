using MediatR;
using POS.Application.Commons.Bases;
using POS.Application.Commons.Select.Response;

namespace POS.Application.UseCases.DocumentType.Queries.GetSelectQuery;

public class GetSelectDocumentTypeQuery : IRequest<BaseResponse<IEnumerable<SelectResponse>>>
{
}