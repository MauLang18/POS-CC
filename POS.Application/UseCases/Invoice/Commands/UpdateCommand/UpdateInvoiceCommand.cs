using MediatR;
using POS.Application.Commons.Bases;

namespace POS.Application.UseCases.Invoice.Commands.UpdateCommand;

public class UpdateInvoiceCommand : IRequest<BaseResponse<bool>>
{
    public int InvoiceId { get; set; }
    public int VoucherTypeId { get; set; }
    public string? VoucherNumber { get; set; }
    public int SaleId { get; set; }
    public int InstallmentsCount { get; set; }
    public int PaymentMethodId { get; set; }
    public int StatusId { get; set; }
    public DateTime PaymentDate { get; set; }
    public DateTime IssueDate { get; set; }
}