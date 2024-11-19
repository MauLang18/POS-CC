using MediatR;
using POS.Application.Commons.Bases;

namespace POS.Application.UseCases.Quote.Commands.UpdateCommand;

public class UpdateQuoteCommand : IRequest<BaseResponse<bool>>
{
    public int QuoteId { get; set; }
    public int StatusId { get; set; }
}