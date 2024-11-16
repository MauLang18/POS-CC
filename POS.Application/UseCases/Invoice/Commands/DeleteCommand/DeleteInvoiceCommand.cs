using MediatR;
using POS.Application.Commons.Bases;

namespace POS.Application.UseCases.Invoice.Commands.DeleteCommand;

public class DeleteInvoiceCommand : IRequest<BaseResponse<bool>>
{
    public int InvoiceId { get; set; }
}