using MediatR;
using POS.Application.Commons.Bases;
using POS.Application.Dtos.Quote.Response;

namespace POS.Application.UseCases.Quote.Queries.GetByIdQuery;

public class GetQuoteByIdQuery : IRequest<BaseResponse<QuoteByIdResponseDto>>
{
    public int QuoteId { get; set; }
}