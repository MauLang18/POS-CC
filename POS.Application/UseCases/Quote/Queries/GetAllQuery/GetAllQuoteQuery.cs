using MediatR;
using POS.Application.Commons.Bases;
using POS.Application.Dtos.Quote.Response;

namespace POS.Application.UseCases.Quote.Queries.GetAllQuery;

public class GetAllQuoteQuery : BaseFilters, IRequest<BaseResponse<IEnumerable<QuoteResponseDto>>>
{
}