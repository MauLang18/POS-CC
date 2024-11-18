using MediatR;
using POS.Application.Commons.Bases;
using POS.Application.Interfaces.Services;
using POS.Application.UseCases.Invoice.Commands.CreateCommand;
using POS.Utilities.Static;
using WatchDog;
using Entity = POS.Domain.Entities;

public class CreateInvoiceHandler : IRequestHandler<CreateInvoiceCommand, BaseResponse<bool>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IGenerateCodeService _generateCodeService;

    public CreateInvoiceHandler(IUnitOfWork unitOfWork, IGenerateCodeService generateCodeService)
    {
        _unitOfWork = unitOfWork;
        _generateCodeService = generateCodeService;
    }

    public async Task<BaseResponse<bool>> Handle(CreateInvoiceCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<bool>();

        using var transaction = _unitOfWork.BeginTransaction();

        try
        {
            var installmentCount = request.InstallmentsCount;
            if (installmentCount < 1) installmentCount = 1;

            var invoices = new List<Entity.Invoice>();
            var sale = _unitOfWork.Sale.GetByIdAsync(request.SaleId);

            for (int i = 1; i <= installmentCount; i++)
            {
                var invoice = new Entity.Invoice
                {
                    SaleId = request.SaleId,
                    Total = Math.Round(sale.Result.Total / installmentCount, 2),
                    InstallmentsCount = request.InstallmentsCount,
                    PaymentMethodId = request.PaymentMethodId,
                    StatusId = request.StatusId,
                    VoucherTypeId = request.VoucherTypeId,
                    State = (int)StateTypes.Activo,

                    VoucherNumber = installmentCount > 1
                        ? $"{await _generateCodeService.GenerateCodeInvoice(request.SaleId)}-{i}"
                        : await _generateCodeService.GenerateCodeInvoice(request.SaleId)
                };

                invoices.Add(invoice);
            }

            foreach (var invoice in invoices)
            {
                await _unitOfWork.Invoice.CreateAsync(invoice);
            }

            await _unitOfWork.SaveChangesAsync();

            transaction.Commit();
            response.IsSuccess = true;
            response.Message = ReplyMessage.MESSAGE_SAVE;
        }
        catch (Exception ex)
        {
            transaction.Rollback();
            response.IsSuccess = false;
            response.Message = ex.Message;
            WatchLogger.LogError(ex.Message);
        }

        return response;
    }
}