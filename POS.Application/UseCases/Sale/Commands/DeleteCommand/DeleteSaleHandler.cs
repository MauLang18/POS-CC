using MediatR;
using POS.Application.Commons.Bases;
using POS.Application.Interfaces.Services;
using POS.Application.UseCases.Sale.Queries.GetByIdQuery;
using POS.Utilities.Static;
using WatchDog;

namespace POS.Application.UseCases.Sale.Commands.DeleteCommand;

public class DeleteSaleHandler : IRequestHandler<DeleteSaleCommand, BaseResponse<bool>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMediator _mediator;

    public DeleteSaleHandler(IUnitOfWork unitOfWork, IMediator mediator)
    {
        _unitOfWork = unitOfWork;
        _mediator = mediator;
    }

    public async Task<BaseResponse<bool>> Handle(DeleteSaleCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<bool>();

        using var transaction = _unitOfWork.BeginTransaction();

        try
        {
            var sale = await _mediator.Send(new GetSaleByIdQuery { SaleId = request.SaleId }, cancellationToken);

            if (sale is null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                return response;
            }

            await _unitOfWork.Sale.DeleteAsync(request.SaleId);
            await _unitOfWork.SaveChangesAsync();

            foreach (var detail in sale.Data!.SaleDetails)
            {
                var productService = await _unitOfWork.ProductService.GetByIdAsync(detail.ProductServiceId);

                if(productService.IsService.Equals((int)ServiceType.Producto))
                    productService.StockQuantity += detail.Quantity;

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