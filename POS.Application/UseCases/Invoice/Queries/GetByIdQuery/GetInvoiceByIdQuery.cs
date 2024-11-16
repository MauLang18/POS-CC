using MediatR;
using POS.Application.Commons.Bases;
using POS.Application.Dtos.Invoice.Response;

namespace POS.Application.UseCases.Invoice.Queries.GetByIdQuery;

public class GetInvoiceByIdQuery : IRequest<BaseResponse<InvoiceByIdResponseDto>>
{
    public int InvoiceId { get; set; }
}