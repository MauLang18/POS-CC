using MediatR;
using POS.Application.Commons.Bases;

namespace POS.Application.UseCases.Quote.Commands.DeleteCommand;

public class DeleteQuoteCommand : IRequest<BaseResponse<bool>>
{
    public int QuoteId { get; set; }
}