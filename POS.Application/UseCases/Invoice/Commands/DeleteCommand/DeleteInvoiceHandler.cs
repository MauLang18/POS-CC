using MediatR;
using POS.Application.Commons.Bases;
using POS.Application.Interfaces.Services;
using POS.Application.UseCases.Invoice.Queries.GetByIdQuery;
using POS.Utilities.Static;
using WatchDog;

namespace POS.Application.UseCases.Invoice.Commands.DeleteCommand;

public class DeleteInvoiceHandler : IRequestHandler<DeleteInvoiceCommand, BaseResponse<bool>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMediator _mediator;

    public DeleteInvoiceHandler(IUnitOfWork unitOfWork, IMediator mediator)
    {
        _unitOfWork = unitOfWork;
        _mediator = mediator;
    }

    public async Task<BaseResponse<bool>> Handle(DeleteInvoiceCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<bool>();

        using var transaction = _unitOfWork.BeginTransaction();

        try
        {
            var invoice = await _mediator.Send(new GetInvoiceByIdQuery { InvoiceId = request.InvoiceId }, cancellationToken);

            if (invoice is null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                return response;
            }

            await _unitOfWork.Invoice.DeleteAsync(request.InvoiceId);
            await _unitOfWork.SaveChangesAsync();

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