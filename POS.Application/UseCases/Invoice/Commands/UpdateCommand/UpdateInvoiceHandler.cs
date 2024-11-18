using AutoMapper;
using MediatR;
using POS.Application.Commons.Bases;
using POS.Application.Interfaces.Services;
using POS.Utilities.Static;
using WatchDog;
using Entity = POS.Domain.Entities;

namespace POS.Application.UseCases.Invoice.Commands.UpdateCommand;

public class UpdateInvoiceHandler : IRequestHandler<UpdateInvoiceCommand, BaseResponse<bool>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateInvoiceHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BaseResponse<bool>> Handle(UpdateInvoiceCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<bool>();

        try
        {
            var existInvoice = await _unitOfWork.Invoice.GetByIdAsync(request.InvoiceId);

            if (existInvoice is null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                return response;
            }

            var issueDate = DateTime.SpecifyKind(DateTime.Parse(request.IssueDate.ToString()), DateTimeKind.Utc);
            var paymentDate = DateTime.SpecifyKind(DateTime.Parse(request.PaymentDate.ToString()), DateTimeKind.Utc);

            var invoice = _mapper.Map<Entity.Invoice>(request);
            invoice.Id = request.InvoiceId;
            invoice.VoucherNumber = existInvoice.VoucherNumber;
            invoice.IssueDate = issueDate;
            invoice.PaymentDate = paymentDate;

            if (!existInvoice.SaleId.Equals(request.SaleId))
            {
                var sale = await _unitOfWork.Sale.GetByIdAsync(request.SaleId);
                invoice.Total = Math.Round(sale.Total / request.InstallmentsCount, 2);
            }
            else
            {
                invoice.Total = existInvoice.Total;
            }

            _unitOfWork.Invoice.UpdateAsync(invoice);
            await _unitOfWork.SaveChangesAsync();

            response.IsSuccess = true;
            response.Message = ReplyMessage.MESSAGE_UPDATE;
        }
        catch (Exception ex)
        {
            response.Message = ex.Message;
            WatchLogger.LogError(ex.Message);
        }

        return response;
    }
}