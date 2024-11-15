using AutoMapper;
using MediatR;
using POS.Application.Commons.Bases;
using POS.Application.Interfaces.Services;
using POS.Domain.Entities;
using POS.Utilities.Static;
using WatchDog;
using Entity = POS.Domain.Entities;

namespace POS.Application.UseCases.Sale.Commands.CreateCommand;

public class CreateSaleHandler : IRequestHandler<CreateSaleCommand,BaseResponse<bool>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IGenerateCodeService _generateCodeService;

    public CreateSaleHandler(IUnitOfWork unitOfWork, IMapper mapper, IGenerateCodeService generateCodeService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _generateCodeService = generateCodeService;
    }

    public async Task<BaseResponse<bool>> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<bool>();

        using var transaction = _unitOfWork.BeginTransaction();

        try
        {
            var sale = _mapper.Map<Entity.Sale>(request);
            sale.State = (int)StateTypes.Activo;

            sale.VoucherNumber = await _generateCodeService.GenerateCodeSale(sale.Id);

            await _unitOfWork.Sale.CreateAsync(sale);
            await _unitOfWork.SaveChangesAsync();

            foreach (var detail in sale.SaleDetails)
            {
                var productService = await _unitOfWork.ProductService.GetByIdAsync(detail.ProductServiceId);

                if (productService.IsService.Equals((int)ServiceType.Producto))
                    productService.StockQuantity -= detail.Quantity;

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