using MediatR;
using POS.Application.Commons.Bases;
using POS.Application.Interfaces.Services;
using POS.Application.UseCases.Purchase.Queries.GetByIdQuery;
using POS.Utilities.Static;
using WatchDog;

namespace POS.Application.UseCases.Purchase.Commands.DeleteCommand;

public class DeletePurchaseHandler : IRequestHandler<DeletePurchaseCommand, BaseResponse<bool>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMediator _mediator;

    public DeletePurchaseHandler(IUnitOfWork unitOfWork, IMediator mediator)
    {
        _unitOfWork = unitOfWork;
        _mediator = mediator;
    }

    public async Task<BaseResponse<bool>> Handle(DeletePurchaseCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<bool>();

        using var transaction = _unitOfWork.BeginTransaction();

        try
        {
            var purchase = await _mediator.Send(new GetPurchaseByIdQuery { PurchaseId = request.PurchaseId }, cancellationToken);

            if (purchase is null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                return response;
            }

            await _unitOfWork.Purchase.DeleteAsync(request.PurchaseId);
            await _unitOfWork.SaveChangesAsync();

            foreach (var detail in purchase.Data!.PurchaseDetails)
            {
                var productService = await _unitOfWork.ProductService.GetByIdAsync(detail.ProductServiceId);

                if (productService.IsService.Equals((int)ServiceType.Producto))
                    productService.StockQuantity -= detail.Quantity;

                _unitOfWork.ProductService.UpdateAsync(productService);
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