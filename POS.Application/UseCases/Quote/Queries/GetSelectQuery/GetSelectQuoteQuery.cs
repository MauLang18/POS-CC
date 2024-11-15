using MediatR;
using POS.Application.Commons.Bases;
using POS.Application.Commons.Select.Response;

namespace POS.Application.UseCases.Quote.Queries.GetSelectQuery;

public class GetSelectQuoteQuery : IRequest<BaseResponse<IEnumerable<SelectResponse>>>
{
}