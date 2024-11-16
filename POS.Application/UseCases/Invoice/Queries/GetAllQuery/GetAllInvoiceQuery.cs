using MediatR;
using POS.Application.Commons.Bases;
using POS.Application.Dtos.Invoice.Response;

namespace POS.Application.UseCases.Invoice.Queries.GetAllQuery;

public class GetAllInvoiceQuery : BaseFilters, IRequest<BaseResponse<IEnumerable<InvoiceResponseDto>>>
{
}