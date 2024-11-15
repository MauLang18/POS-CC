using AutoMapper;
using MediatR;
using POS.Application.Commons.Bases;
using POS.Application.Interfaces.Services;
using POS.Utilities.Static;
using WatchDog;
using Entity = POS.Domain.Entities;

namespace POS.Application.UseCases.Purchase.Commands.CreateCommand;

public class CreatePurchaseHandler : IRequestHandler<CreatePurchaseCommand, BaseResponse<bool>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreatePurchaseHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BaseResponse<bool>> Handle(CreatePurchaseCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<bool>();

        using var transaction = _unitOfWork.BeginTransaction();

        try
        {
            var purchase = _mapper.Map<Entity.Purchase>(request);
            purchase.State = (int)StateTypes.Activo;

            await _unitOfWork.Purchase.CreateAsync(purchase);
            await _unitOfWork.SaveChangesAsync();

            foreach (var detail in purchase.PurchaseDetails)
            {
                var productService = await _unitOfWork.ProductService.GetByIdAsync(detail.ProductServiceId);

                if (productService.IsService.Equals((int)ServiceType.Producto))
                    productService.StockQuantity -= detail.Quatity;

                _unitOfWork.ProductService.UpdateAsync(productService);
                await _unitOfWork.SaveChangesAsync();
            }

            transaction.Commit();
            response.IsSuccess = true;
            response.Message = ReplyMessage.MESSAGE_SAVE;
        }
        catch (Exception ex)
        {
            response.Message = ex.Message;
            WatchLogger.LogError(ex.Message);
        }

        return response;
    }
}