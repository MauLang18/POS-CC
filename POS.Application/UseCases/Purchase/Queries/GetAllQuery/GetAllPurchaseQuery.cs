using MediatR;
using POS.Application.Commons.Bases;
using POS.Application.Dtos.Purchase.Response;

namespace POS.Application.UseCases.Purchase.Queries.GetAllQuery;

public class GetAllPurchaseQuery : BaseFilters, IRequest<BaseResponse<IEnumerable<PurchaseResponseDto>>>
{
}