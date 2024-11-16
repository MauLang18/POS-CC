using AutoMapper;
using MediatR;
using POS.Application.Commons.Bases;
using POS.Application.Interfaces.Services;
using POS.Domain.Entities;
using POS.Utilities.Static;
using WatchDog;
using Entity = POS.Domain.Entities;

namespace POS.Application.UseCases.Invoice.Commands.CreateCommand;

public class CreateInvoiceHandler : IRequestHandler<CreateInvoiceCommand, BaseResponse<bool>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IGenerateCodeService _generateCodeService;

    public CreateInvoiceHandler(IUnitOfWork unitOfWork, IMapper mapper, IGenerateCodeService generateCodeService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
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
            var totalAmount = request.Total;

            for (int i = 1; i <= installmentCount; i++)
            {
                var invoice = _mapper.Map<Entity.Invoice>(request);
                invoice.State = (int)StateTypes.Activo;

                invoice.VoucherNumber = installmentCount > 1
                    ? $"{await _generateCodeService.GenerateCodeInvoice(invoice.Id)}-{i}"
                    : await _generateCodeService.GenerateCodeInvoice(invoice.Id);

                invoice.Total = Math.Round(totalAmount / installmentCount, 2);

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