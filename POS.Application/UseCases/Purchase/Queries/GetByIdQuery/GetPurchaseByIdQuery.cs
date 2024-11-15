using MediatR;
using POS.Application.Commons.Bases;
using POS.Application.Dtos.Purchase.Response;

namespace POS.Application.UseCases.Purchase.Queries.GetByIdQuery;

public class GetPurchaseByIdQuery : IRequest<BaseResponse<PurchaseByIdResponseDto>>
{
    public int PurchaseId { get; set; }
}