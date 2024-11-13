using MediatR;
using POS.Application.Commons.Bases;
using POS.Application.Interfaces.Services;
using POS.Application.UseCases.Quote.Queries.GetByIdQuery;
using POS.Utilities.Static;
using WatchDog;

namespace POS.Application.UseCases.Quote.Commands.DeleteCommand;

public class DeleteQuoteHandler : IRequestHandler<DeleteQuoteCommand, BaseResponse<bool>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMediator _mediator;

    public DeleteQuoteHandler(IUnitOfWork unitOfWork, IMediator mediator)
    {
        _unitOfWork = unitOfWork;
        _mediator = mediator;
    }

    public async Task<BaseResponse<bool>> Handle(DeleteQuoteCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<bool>();

        using var transaction = _unitOfWork.BeginTransaction();

        try
        {
            var quote = await _mediator.Send(new GetQuoteByIdQuery { QuoteId = request.QuoteId }, cancellationToken);

            if (quote is null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                return response;
            }

            await _unitOfWork.Quote.DeleteAsync(request.QuoteId);
            await _unitOfWork.SaveChangesAsync();

            foreach (var detail in quote.Data!.QuoteDetails)
            {
                var productServiceState = await _unitOfWork.ProductService.GetByIdAsync(detail.ProductServiceId);
                productServiceState.State = 1;
                _unitOfWork.ProductService.UpdateAsync(productServiceState);
                await _unitOfWork.SaveChangesAsync();
            }

            transaction.Commit();
            response.IsSuccess = true;
            response.Message = ReplyMessage.MESSAGE_DELETE;
        }
        catch (Exception ex)
        {
            response.Message = ex.Message;
            WatchLogger.LogError(ex.Message);
        }

        return response;
    }
}